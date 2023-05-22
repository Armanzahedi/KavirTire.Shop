using KavirTire.Shop.Domain.IPGs.Enums;

namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway;

public static class PaymentGatewayHelper
{
    public static List<Type> GatewayTypes;

    static PaymentGatewayHelper()
    {
        var assembly = typeof(ServiceRegistration).Assembly;
        GatewayTypes = assembly.GetExportedTypes()
            .Where(t => t.IsClass && t.BaseType == typeof(Application.Payments.Services.PaymentGateway.PaymentGateway))
            .ToList();
    }

    public static bool HasBankTypeAttribute(this Type type, Bank bank)
    {
        var attribute = type.GetCustomAttributes(typeof(BankTypeAttribute), false).FirstOrDefault();
        return attribute != null && ((BankTypeAttribute)attribute).Bank == bank;
    }

}