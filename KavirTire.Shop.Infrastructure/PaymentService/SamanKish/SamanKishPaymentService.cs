using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Payments.Services.PaymentService;
using KavirTire.Shop.Application.Payments.Specifications;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Domain.IPGs.Entities;
using KavirTire.Shop.Domain.IPGs.Enums;
using KavirTire.Shop.Domain.Payments;
using KavirTire.Shop.Infrastructure.PaymentIFBinding;
using KavirTire.Shop.Infrastructure.PaymentService.SamanKish.Models;
using Newtonsoft.Json;
using RestSharp;
using GetTokenResponse = KavirTire.Shop.Infrastructure.PaymentService.SamanKish.Models.GetTokenResponse;

namespace KavirTire.Shop.Infrastructure.PaymentService.SamanKish;

[BankType(Bank.Saman)]
public class SamanKishPaymentService : Application.Payments.Services.PaymentService.PaymentService
{
    private readonly Ipg _ipg;
    private readonly IRepository<Payment> _paymentRepo;

    public SamanKishPaymentService(Ipg ipg, IRepository<Payment> paymentRepo) : base(ipg)
    {
        _ipg = ipg;
        _paymentRepo = paymentRepo;
    }

    private string _baseUrl => "https://sep.shaparak.ir";
    private string _getTokenEndpoint => $"/OnlinePG/OnlinePG";
    private string _getUrlBase => "https://sep.shaparak.ir/OnlinePG/SendToken?token=";
    
    public override Task<string> GetGatewayUrl(Invoice invoice,  Payment payment,BankAccount bankAccount, BankAccount? postBankAccount, string? customerMobileNumber = "09921744820")
    {
        
        var sepTxn = new SepTxn()
        {
            Amount = Convert.ToInt64(invoice.TotalCost),
            TotalAmount = Convert.ToInt64(invoice.TotalCost),
            ResNum = payment.ResNo,
            CellNumber = customerMobileNumber??"",
            RedirectUrl = new Uri(_ipg.ReturnUrl)
                .AddParameter("pmnt",payment.Id.ToString())
                .AddParameter("inv",invoice.Id.ToString())
                .AddParameter("ipg",_ipg.Id.ToString()).ToString(),
            TerminalId = _ipg.TerminalId
        };
         
        if (postBankAccount?.Iban != null)
        {
            var ibanInfoBankAccount = new IBANInfo()
            {
                Amount = (int)invoice.TotalInventoryItemCost,
                IBAN = bankAccount.Iban
            };
            var ibanInfoPostBankAccount = new IBANInfo()
            {
                Amount = Convert.ToInt64(invoice.TotalPostCost), 
                IBAN = postBankAccount.Iban
            };
            sepTxn.SettleMentIBANInfo = new List<IBANInfo> { ibanInfoBankAccount, ibanInfoPostBankAccount };
        }

        var tokenResponse = GetToken(sepTxn);
        if (tokenResponse.Status == GetTokenStatus.Failed) 
            throw new Exception(tokenResponse.ErrorDesc);
        
        return Task.FromResult($"{_getUrlBase}{tokenResponse.Token}");
    }

    public override async Task<VerifyTransactionResult> VerifyTransaction(Dictionary<string, string?> bankResponse)
    {
        var bankResponseObj = JsonConvert.DeserializeObject<SepResponse>(JsonConvert.SerializeObject(bankResponse));

        if (bankResponseObj?.RefNum != null)
        {
            var paymentExists = await _paymentRepo.AnyAsync(new PaymentByRefNoSpec(bankResponseObj.RefNum));
            if (paymentExists)
                throw new DoubleSpendingException();
        }
        
        if (bankResponseObj?.Status == SepResponseStatus.OK)
        {
            // var verifyTransactionResponse = await SepVerifyTransaction(new SepVerifyTransactionRequest()
            // { TerminalId = _ipg.TerminalId, RefNum = bandResponseObj.RefNum, Password = _ipg.Password, Amount= bandResponseObj.Amount });

            return new VerifyTransactionResult
            {
                // IsSuccessful = verifyTransactionResponse.isSuccessful,
                // Massage = verifyTransactionResponse.Message,
                IsSuccessful = true,
                Massage = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                RRN = bankResponseObj.RRN,
                ProviderMessage = GetResponseMessage(bankResponseObj.Status),
                RefNum = bankResponseObj.RefNum,
                ResNo = bankResponseObj.ResNum,
                SecurePan = bankResponseObj.SecurePan,
                PaymentIdentity = bankResponseObj.TraceNo,
                Amount = bankResponseObj.Amount,
            };
        }
        return new VerifyTransactionResult
        {
            IsSuccessful = false,
            Massage = "پردازش رسيد ديجيتالي با خطا مواجه شد.",
        };
    }

    public override async Task ReverseTransaction(Dictionary<string, string?> bankResponse)
    {
        var bandResponseObj = JsonConvert.DeserializeObject<SepResponse>(JsonConvert.SerializeObject(bankResponse));
        var sepSrv = new PaymentIFBindingSoapClient(PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);

        for (int i = 0; i < 10; i++)
        {   
            try
            {
                string userName = bandResponseObj.TerminalId;
                string pass = _ipg.Password;
                await sepSrv.reverseTransactionAsync(bandResponseObj.RefNum, bandResponseObj.TerminalId, userName, pass);
                return;
            }
            catch (Exception ex)
            {
                Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                Task.Delay(random.Next(0, 2) * 100).Wait();
            }
        }
    }


    #region Private

       private GetTokenResponse GetToken(SepTxn txn)
        {
            txn.Action = "Token";
            var client = new RestClient("https://sep.shaparak.ir/MobilePG/MobilePayment");
            var request = new RestRequest("",Method.Post) { RequestFormat = DataFormat.Json }
                .AddBody(JsonConvert.SerializeObject(txn));

            var response = client.Execute(request);
            if (!response.IsSuccessful)
                throw new Exception("دریافت توکن با خطا مواجه شد.");
        
            return JsonConvert.DeserializeObject<GetTokenResponse>(response.Content);
        }

        private string GetResponseMessage(SepResponseStatus? responseStatus)
        {
            string message = "";
            switch (responseStatus)
            {
                case SepResponseStatus.CanceledByUser:
                    message = "کاربر انصراف داده است";
                    break;
                case SepResponseStatus.OK:
                    message = "پرداخت با موفقیت انجام شد";
                    break;
                case SepResponseStatus.Failed:
                    message = "پرداخت انجام نشد";
                    break;
                case SepResponseStatus.SessionIsNull:
                    message = "کاربر در بازه زمانی تعیین شده پاسخی ارسال نکرده است";
                    break;
                case SepResponseStatus.InvalidParameters:
                    message = "پارامترهای ارسالی نامعتبر است";
                    break;
                case SepResponseStatus.MerchantIpAddressIsInvalid:
                    message = "آدرس سرور پذیرنده نامعتبر است";
                    break;
                case SepResponseStatus.TokenNotFound:
                    message = "توکن ارسال شده یافت نشد ";
                    break;
                case SepResponseStatus.TokenRequired:
                    message = "با این شماره ترمینال فقط تراکنش های توکنی قابل پرداخت هستند";
                    break;
                case SepResponseStatus.TerminalNotFound:
                    message = "شماره ترمینال ارسال شده یافت نشد ";
                    break;
            }

            return message;
        }

        private string TransactionChecking(int i)
        {
            bool isError = false;
            string errorMsg = "";
            switch (i)
            {

                case -1:		//TP_ERROR
                    isError = true;
                    errorMsg = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-1";
                    break;
                case -2:		//ACCOUNTS_DONT_MATCH
                    isError = true;
                    errorMsg = "بروز خطا در هنگام تاييد رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-2";
                    break;
                case -3:		//BAD_INPUT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-3";
                    break;
                case -4:		//INVALID_PASSWORD_OR_ACCOUNT
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-4";
                    break;
                case -5:		//DATABASE_EXCEPTION
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-5";
                    break;
                case -7:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-7";
                    break;
                case -8:		//ERROR_STR_TOO_LONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-8";
                    break;
                case -9:		//ERROR_STR_NOT_AL_NUM
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-9";
                    break;
                case -10:	//ERROR_STR_NOT_BASE64
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-10";
                    break;
                case -11:	//ERROR_STR_TOO_SHORT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-11";
                    break;
                case -12:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-12";
                    break;
                case -13:		//ERROR IN AMOUNT OF REVERS TRANSACTION AMOUNT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-13";
                    break;
                case -14:	//INVALID TRANSACTION
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-14";
                    break;
                case -15:	//RETERNED AMOUNT IS WRONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-15";
                    break;
                case -16:	//INTERNAL ERROR
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-16";
                    break;
                case -17:	// REVERS TRANSACTIN FROM OTHER BANK
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-17";
                    break;
                case -18:	//INVALID IP
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-18";
                    break;
            }
            return errorMsg;
        }

        private async Task<(bool isSuccessful, string Message, double Amount)> SepVerifyTransaction(SepVerifyTransactionRequest verifyTransactionRequest)
        {

            var srv = new PaymentIFBindingSoapClient(PaymentIFBindingSoapClient.EndpointConfiguration.PaymentIFBindingSoap);
            for (int i = 0; i < 10; i++)
            {   
                try
                {
                    var retrievedAmount = await srv.verifyTransactionAsync(verifyTransactionRequest.RefNum, verifyTransactionRequest.TerminalId);
                    if (retrievedAmount > 0)
                    {
                        if (retrievedAmount == verifyTransactionRequest.Amount)
                        {
                            return (true,$"بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل شد <br/>  شماره رسید : {verifyTransactionRequest.RefNum}", retrievedAmount);
                        }
                        else
                        {
                            string userName = verifyTransactionRequest.TerminalId;
                            string pass = verifyTransactionRequest.Password;
                            await srv.reverseTransactionAsync(verifyTransactionRequest.RefNum, verifyTransactionRequest.TerminalId, userName, pass);
                            return (false,$"دریافت اطلاعات از بانک با مشکل مواجه شد و مبلغ پرداخت شده ظرف 72 ساعت آینده به حساب شما برگشت داده می شود.", retrievedAmount);
                        }
                    }
                    
                    return (false,TransactionChecking((int)retrievedAmount), retrievedAmount);
                }
                catch (Exception ex)
                {
                    Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    Task.Delay(random.Next(0, 2) * 100).Wait();
                }
            }

            return (false,"خطا در پردازش رسيد ديجيتالي.", -6);
        }
    #endregion
}