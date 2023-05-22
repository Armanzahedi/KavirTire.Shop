namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway.SamanKish.Models;

public class IBANInfo
{
    public string IBAN { get; set; }
    public long Amount { get; set; }
    public string PurchaseID { get; set; } = "";
}