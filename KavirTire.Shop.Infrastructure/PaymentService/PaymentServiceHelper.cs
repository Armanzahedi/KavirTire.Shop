using KavirTire.Shop.Application.Payments.Services.PaymentService;
using KavirTire.Shop.Domain.IPGs.Enums;
using KavirTire.Shop.Infrastructure.PaymentService.SamanKish;

namespace KavirTire.Shop.Infrastructure.PaymentService;

public static class PaymentServiceHelper
{
    public static List<Type> GatewayTypes;

    static PaymentServiceHelper()
    {
        var assembly = typeof(ServiceRegistration).Assembly;
        GatewayTypes = assembly.GetExportedTypes()
            .Where(t => t.IsClass && t.BaseType == typeof(Application.Payments.Services.PaymentService.PaymentService))
            .ToList();
    }

    public static bool HasBankTypeAttribute(this Type type, Bank bank)
    {
        var attribute = type.GetCustomAttributes(typeof(BankTypeAttribute), false).FirstOrDefault();
        return attribute != null && ((BankTypeAttribute)attribute).Bank == bank;
    }

}