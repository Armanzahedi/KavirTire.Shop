using Ardalis.Specification;
using KavirTire.Shop.Domain.Invoices;

namespace KavirTire.Shop.Application.Invoices.Specifications;

public class InvoiceByIdSpec :Specification<Invoice>
{
    public InvoiceByIdSpec(Guid invoiceId)
    {
        Query.Where(x => x.Id == invoiceId);
    }
}