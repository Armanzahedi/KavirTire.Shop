using KavirTire.Shop.Domain.IPGs.Enums;

namespace KavirTire.Shop.Presentation.Models.Payment;

public class PaymentRedirect
{
    public PaymentRedirect(Bank bank,string redirectUrl)
    {
        this.Bank = bank;
        this.RedirectUrl = redirectUrl;
    }
    public Bank Bank { get; set; }
    public string RedirectUrl { get; set; }
}