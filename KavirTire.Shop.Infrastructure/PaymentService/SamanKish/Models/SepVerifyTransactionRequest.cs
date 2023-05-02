namespace KavirTire.Shop.Infrastructure.PaymentService.SamanKish.Models;

public class SepVerifyTransactionRequest
{
    public long Amount { get; set; }
    public string RefNum { get; set; }
    public string TerminalId { get; set; }
    public string Password { get; set; } 
}