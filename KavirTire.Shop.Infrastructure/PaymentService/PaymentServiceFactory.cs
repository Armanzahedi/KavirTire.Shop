using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Payments.Services.PaymentService;
using KavirTire.Shop.Domain.IPGs;
using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.PaymentService;

public class PaymentServiceFactory: IPaymentServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PaymentServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Application.Payments.Services.PaymentService.PaymentService Create(Ipg ipg)
    {
        Guard.Against.Null(ipg.Bank, nameof(ipg.Bank));
        var gateway = PaymentServiceHelper.GatewayTypes.FirstOrDefault(t=>t.HasBankTypeAttribute(ipg.Bank.Value));
        if(gateway == null)
            throw new Exception($"Payment gateway with bank type of {ipg.Bank} was not found");

        var ctor = gateway.GetConstructors().FirstOrDefault();
        object?[] ctorArgs = {};

        
        foreach (var param in ctor.GetParameters())
        {
            ctorArgs = ctorArgs.Append(param.ParameterType == typeof(Ipg)
                ? ipg
                : _serviceProvider.GetService(param.ParameterType)).ToArray();
        }
        return (Application.Payments.Services.PaymentService.PaymentService)Activator.CreateInstance(gateway, ctorArgs)!;
    }
}