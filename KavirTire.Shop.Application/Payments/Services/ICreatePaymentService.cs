using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Domain.Payments;

namespace KavirTire.Shop.Application.Payments.Services;

public interface ICreatePaymentService
{
    Task<Payment> CreatePayment(Invoice invoice,Ipg ipg, Guid bankAccountId, CancellationToken cancellationToken = new());
}