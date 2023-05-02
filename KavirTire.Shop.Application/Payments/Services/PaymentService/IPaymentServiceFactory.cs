using KavirTire.Shop.Domain.IPGs;

namespace KavirTire.Shop.Application.Payments.Services.PaymentService;

public interface IPaymentServiceFactory
{
    PaymentService Create(Ipg ipg);
}