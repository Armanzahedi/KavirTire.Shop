using Ardalis.Specification;
using KavirTire.Shop.Domain.Invoices;

namespace KavirTire.Shop.Application.Common.Specifications;

public class InvoiceWithInvoiceItemsByIdSpec : Specification<Invoice>
{
    public InvoiceWithInvoiceItemsByIdSpec(Guid invoiceId)
    {
        Query
            .AsNoTracking()
            .Where(x => x.Id == invoiceId)
            .Include(x=>x.InvoiceItems);
    }
}