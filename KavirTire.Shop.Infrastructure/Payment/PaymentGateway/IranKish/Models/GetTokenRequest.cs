namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway.IranKish.Models;

public class GetTokenRequest
{
    public AuthenticationEnvelope AuthenticationEnvelope = new();
    public TokenRequest Request = new();
}
public class TokenRequest
{
    public string AcceptorId { get; set; }
    public long amount { get; set; }
    public BillInfo BillInfo { get; set; }
    public string CmsPreservationId { get; set; }
    public List<MultiplexParameter> multiplexParameters { get; set; }
    public string PaymentId { get; set; }
    public string RequestId { get; set; }
    public long RequestTimestamp { get; set; }
    public string RevertUri { get; set; }
    public string terminalId { get; set; }
    public string transactionType { get; set; }
    public List<KeyValuePair<string, string>> additionalParameters { get; set; }
}
public class AuthenticationEnvelope
{
    public string Data { get; set; }
    public string Iv { get; set; }
}