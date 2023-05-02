using Ardalis.Specification;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.Invoices.Enums;

namespace KavirTire.Shop.Application.Invoices.Specifications;

public class InvoiceWithInvoiceItemsByStatusSpec : Specification<Invoice>
{
    public InvoiceWithInvoiceItemsByStatusSpec(InvoiceStatus invoiceStatus)
    {
        Query
            .Include(x=>x.InvoiceItems)
            .Where(x => x.InvoiceStatus == invoiceStatus);
    }
}