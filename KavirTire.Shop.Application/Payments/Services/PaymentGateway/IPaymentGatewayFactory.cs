using KavirTire.Shop.Domain.IPGs;

namespace KavirTire.Shop.Application.Payments.Services.PaymentGateway;

public interface IPaymentGatewayFactory
{
    PaymentGateway Create(Ipg ipg);
}