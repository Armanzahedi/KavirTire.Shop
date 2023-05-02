namespace KavirTire.Shop.Infrastructure.PaymentService.SamanKish.Models;

public class IBANInfo
{
    public string IBAN { get; set; }
    public long Amount { get; set; }
    public string PurchaseID { get; set; } = "";
}