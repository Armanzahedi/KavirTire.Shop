using Ardalis.Specification;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.Invoices.Enums;

namespace KavirTire.Shop.Application.Invoices.Specifications;

public class ExpiredInvoiceWithItemsSpec : Specification<Invoice>
{
    public ExpiredInvoiceWithItemsSpec()
    {
        Query
            .Include(x=>x.InvoiceItems)
            .Where(x =>
            x.InvoiceStatus == InvoiceStatus.Draft &&
            x.ExpirationDate != null &&
            x.ExpirationDate.Value <= DateTime.Now);
    }
}