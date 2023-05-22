using KavirTire.Shop.Domain.IPGs.Enums;

namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway;

[AttributeUsage(AttributeTargets.Class)]
public class BankTypeAttribute : Attribute
{
    public Bank Bank { get; }

    public BankTypeAttribute(Bank bank)
    {
        Bank = bank;
    }
}