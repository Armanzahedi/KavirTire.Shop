using Ardalis.Specification;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.Invoices.Enums;

namespace KavirTire.Shop.Application.Invoices.Specifications;

// public class ExpiredPendingInvoiceWithItemsSpec :Specification<Invoice>
// {
//     public ExpiredPendingInvoiceWithItemsSpec()
//     {
//         Query
//             .Include(x=>x.InvoiceItems)
//             .Where(x => x.InvoiceStatus == InvoiceStatus.Draft &&
//                         
//                          x.CreateDate.AddMinutes(20) <= DateTime.Now);
//     }
// }