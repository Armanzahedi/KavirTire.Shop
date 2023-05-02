namespace KavirTire.Shop.Infrastructure.PaymentService.SamanKish.Models;

public class SepTxn
{
    public string Action { get; set; }
    public string TerminalId { get; set; }
    public string RedirectUrl { get; set; }
    public string ResNum { get; set; }
    public string ResNum1 { get; set; }
    public string ResNum2 { get; set; }
    public string ResNum3 { get; set; }
    public string ResNum4 { get; set; }
    public long Amount { get; set; }
    public long TotalAmount { get; set; }
    public string CellNumber { get; set; }
    public List<IBANInfo>? SettleMentIBANInfo { get; set; }
}