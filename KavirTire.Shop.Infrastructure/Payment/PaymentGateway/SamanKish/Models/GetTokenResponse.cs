namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway.SamanKish.Models;

public class GetTokenResponse
{
    public GetTokenStatus Status { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorDesc { get; set; }
    public string Token { get; set; }
}