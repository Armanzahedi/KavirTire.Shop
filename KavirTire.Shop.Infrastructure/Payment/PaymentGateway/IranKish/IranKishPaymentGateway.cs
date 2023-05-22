using System.Security.Cryptography;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Payments.Services.PaymentGateway;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Domain.IPGs.Entities;
using KavirTire.Shop.Domain.IPGs.Enums;
using KavirTire.Shop.Infrastructure.Payment.PaymentGateway.IranKish.Models;
using Newtonsoft.Json;
using RestSharp;

namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway.IranKish;

[BankType(Bank.IranKish)]
public class IranKishPaymentGateway : Application.Payments.Services.PaymentGateway.PaymentGateway 
{
    private readonly Ipg _ipg;

    public IranKishPaymentGateway(Ipg ipg) : base(ipg)
    {
        _ipg = ipg;
    }

    private string _baseUrl => "https://ikc.shaparak.ir/api/v3";
    private string _getTokenEndpoint => $"/tokenization/make";
    private string _verifyTransactionEndpoint => "/confirmation/purchase";
    private string _getUrlBase => "https://ikc.shaparak.ir/iuiv3/IPG/Index#";

    
    public override Task<string> GetGatewayUrl(Invoice invoice,  Domain.Payments.Payment payment, BankAccount bankAccount,BankAccount? postBankAccount,string? customerMobileNumber)
    {
            List<MultiplexParameter> multiplexParameters = new List<MultiplexParameter>();


            MultiplexParameter account1 = new MultiplexParameter();
            account1.Amount = Convert.ToInt32(invoice.TotalInventoryItemCost);
            account1.Iban = bankAccount.Iban;
            multiplexParameters.Add(account1);

            MultiplexParameter account2 = new MultiplexParameter();
            account2.Amount = Convert.ToInt32(invoice.TotalPostCost);
            account2.Iban = postBankAccount.Iban;
            multiplexParameters.Add(account2);


            GetTokenRequest getTokenRequest = new GetTokenRequest
            {
                Request = 
                {
                    AcceptorId = _ipg.AcceptorId,
                    amount = account1.Amount + account2.Amount,
                    multiplexParameters = multiplexParameters,
                    // PaymentId = RandomDigitGenerator.Create(10),
                    RequestId = payment.ResNo,
                    RevertUri = new Uri(_ipg.ReturnUrl)
                        .AddParameter("pmnt",payment.Id.ToString())
                        .AddParameter("inv",invoice.Id.ToString())
                        .AddParameter("ipg",_ipg.Id.ToString()).ToString(),
                    terminalId = _ipg.TerminalId,
                    RequestTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds(),
                    transactionType = TransactionType.Purchase
                }
            };

            CreateAESCoding(getTokenRequest, _ipg.RsaKeyValue, _ipg.PassPhrase, getTokenRequest.Request.multiplexParameters == null ? false : true);
            
            var responseApi = GetToken(getTokenRequest);
            return Task.FromResult(_getUrlBase + responseApi.result.token);
    }

    public override async Task<VerifyTransactionResult> VerifyTransaction(Dictionary<string, string?> bankResponse)
    {
        var bandResponseObj = JsonConvert.DeserializeObject<IranKishResponse>(JsonConvert.SerializeObject(bankResponse));

        var verifyRequest = new IranKishVerifyTransactionRequest
        {
            terminalId = _ipg.TerminalId,
            systemTraceAuditNumber = bandResponseObj.systemTraceAuditNumber,
            retrievalReferenceNumber = bandResponseObj.retrievalReferenceNumber,
            tokenIdentity = bandResponseObj.token
        };
        var client = new RestClient(_baseUrl);
        var request = new RestRequest(_verifyTransactionEndpoint, Method.Post) { RequestFormat = DataFormat.Json }
            .AddBody(JsonConvert.SerializeObject(verifyRequest));

        var response = await client.ExecuteAsync<IranKishVerifyTransactionResponse>(request);
        
        return new VerifyTransactionResult
        {
            IsSuccessful = response.IsSuccessful,
            Massage = response.Data?.description
        };
    }

    public override Task ReverseTransaction(Dictionary<string, string?> bankResponse)
    {
        return Task.CompletedTask;
    }


    #region Private

    private GetTokenResponse GetToken(GetTokenRequest getTokenRequest)
    {
 
        var client = new RestClient(_baseUrl);
        var request = new RestRequest(_getTokenEndpoint, Method.Post) { RequestFormat = DataFormat.Json }
            .AddBody(JsonConvert.SerializeObject(getTokenRequest));

        var response = client.Execute(request);
        if (!response.IsSuccessful)
            throw new Exception("دریافت توکن با خطا مواجه شد.");

        var result = JsonConvert.DeserializeObject<GetTokenResponse>(response.Content);
        Console.WriteLine(result.responseCode);
        if(result?.status != true)
            throw new Exception("دریافت توکن با خطا مواجه شد.");
        
        return result;
    }
    private void CreateAESCoding(GetTokenRequest getTokenRequest, string rsaPublicKey, string passPhrase, bool isMultiplex)
    {
        try
        {
            string baseString =
                getTokenRequest.Request.terminalId +
                passPhrase +
                getTokenRequest.Request.amount.ToString().PadLeft(12, '0') +
                (isMultiplex ? "01" : "00") +
                (isMultiplex ?
                    getTokenRequest.Request.multiplexParameters
                        .Select(t => $"{t.Iban.Replace("IR", "2718")}{t.Amount.ToString().PadLeft(12, '0')}")
                        .Aggregate((a, b) => $"{a}{b}")
                    : string.Empty);
            using (Aes myAes = Aes.Create())
            {
                myAes.KeySize = 128;
                myAes.GenerateKey();
                myAes.GenerateIV();
                byte[] keyAes = myAes.Key;
                byte[] ivAes = myAes.IV;

                byte[] resultCoding = new byte[48];
                byte[] baseStringbyte = baseString.HexStringToByteArray();

                byte[] encrypted = EncryptStringToBytes_Aes(baseStringbyte, myAes.Key, myAes.IV);
                byte[] hsahash = new SHA256CryptoServiceProvider().ComputeHash(encrypted);

                resultCoding = CombinArray(keyAes, hsahash);

                getTokenRequest.AuthenticationEnvelope.Data = RSAData(resultCoding, rsaPublicKey).ByteArrayToHexString();
                getTokenRequest.AuthenticationEnvelope.Iv = ivAes.ByteArrayToHexString();
            }
        }
        catch (Exception ex)
        {


        }

    }

    private static byte[] RSAData(byte[] aesCodingResult, string publicKey)
    {
        try
        {
            var csp = new RSACryptoServiceProvider();
            csp.FromXmlString(publicKey);

            return csp.Encrypt(aesCodingResult, false);


        }
        catch (Exception ex)
        {

            return null;
        }

    }
    private static byte[] CombinArray(byte[] first, byte[] second)
    {
        byte[] bytes = new byte[first.Length + second.Length];
        Array.Copy(first, 0, bytes, 0, first.Length);
        Array.Copy(second, 0, bytes, first.Length, second.Length);
        return bytes;
    }
    private static byte[] EncryptStringToBytes_Aes(byte[] plainText, byte[] Key, byte[] IV)
    {
        using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
        {
            aesAlg.KeySize = 128;
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            return encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
            // Create the streams used for encryption.

        }

    }
    #endregion
}

public static class Extension
{
    public static byte[] HexStringToByteArray(this string hexString)
        => Enumerable.Range(0, hexString.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(value: hexString.Substring(startIndex: x, length: 2), fromBase: 16))
            .ToArray();

    public static string ByteArrayToHexString(this byte[] bytes)
        => bytes.Select(t => t.ToString(format: "X2")).Aggregate((a, b) => $"{a}{b}");
}