using KavirTire.Shop.Domain.IPGs.Enums;

namespace KavirTire.Shop.Application.Payments.Services.PaymentService;

[AttributeUsage(AttributeTargets.Class)]
public class BankTypeAttribute : Attribute
{
    public Bank Bank { get; }

    public BankTypeAttribute(Bank bank)
    {
        Bank = bank;
    }
}