namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway.SamanKish.Models;

public class SepResponse
{
    public int? MID { get; set; }
    public string? State { get; set; }
    public SepResponseStatus? Status { get; set; }
    public long? RRN { get; set; }
    public string? RefNum { get; set; }
    public string? ResNum { get; set; }
    public string? ResNum1 { get; set; }
    public string? ResNum2 { get; set; }
    public string? ResNum3 { get; set; }
    public string? ResNum4 { get; set; }
    public string? TerminalId { get; set; }
    public string? TraceNo { get; set; }
    public long Amount { get; set; }
    public string? Wage { get; set; }
    public string? SecurePan { get; set; }
    public string? HashedCardNumber { get; set; }
}