namespace KavirTire.Shop.Infrastructure.PaymentService.IranKish.Models;

public class GetTokenResponse
{
    public string responseCode { get; set; }
    public object description { get; set; }
    public bool status { get; set; }
    public TokenResult result { get; set; } = new();
}
public class TokenResult
{
    public string token { get; set; }
    public int initiateTimeStamp { get; set; }
    public int expiryTimeStamp { get; set; }
    public string transactionType { get; set; }
    public BillInfo billInfo { get; set; } = new();
}