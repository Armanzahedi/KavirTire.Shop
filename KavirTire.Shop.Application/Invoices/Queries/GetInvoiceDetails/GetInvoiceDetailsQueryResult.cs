using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.IPGs;

namespace KavirTire.Shop.Application.Invoices.Queries.GetInvoiceDetails;

public class GetInvoiceDetailsQueryResult
{
    public Invoice Invoice { get; set; }
    public List<Ipg>? Ipgs { get; set; }
}