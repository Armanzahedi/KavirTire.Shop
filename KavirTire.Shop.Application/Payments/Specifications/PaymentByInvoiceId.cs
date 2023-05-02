using Ardalis.Specification;

namespace KavirTire.Shop.Application.Payments.Specifications;

public class PaymentByInvoiceId : Specification<Domain.Payments.Payment>
{
    public PaymentByInvoiceId(Guid invoiceId)
    {
        Query.Where(x => x.InvoiceId == invoiceId);
    }
}