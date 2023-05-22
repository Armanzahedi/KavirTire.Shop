using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Payments.Services.PaymentGateway;
using KavirTire.Shop.Domain.IPGs;

namespace KavirTire.Shop.Infrastructure.Payment.PaymentGateway;

public class PaymentGatewayFactory: IPaymentGatewayFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PaymentGatewayFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Application.Payments.Services.PaymentGateway.PaymentGateway Create(Ipg ipg)
    {
        Guard.Against.Null(ipg.Bank, nameof(ipg.Bank));
        var gateway = PaymentGatewayHelper.GatewayTypes.FirstOrDefault(t=>t.HasBankTypeAttribute(ipg.Bank.Value));
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
        return (Application.Payments.Services.PaymentGateway.PaymentGateway)Activator.CreateInstance(gateway, ctorArgs)!;
    }
}