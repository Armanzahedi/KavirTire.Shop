using Ardalis.Specification;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Enums;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Specifications
{
    public class OutOfSyncInvoiceWithInvoiceItemPaymentSpec : Specification<Invoice>
    {
        public OutOfSyncInvoiceWithInvoiceItemPaymentSpec()
        {
            Query
                .Where(x=>(x.InvoiceStatus == InvoiceStatus.Closed ||
                           x.InvoiceStatus == InvoiceStatus.Expired) &&
                          x.SyncStatus == InvoiceSyncStatus.OutOfSync)
                .Include(x => x.InvoiceItems)
                .Include(x => x.Payments)
                .ThenInclude(x=>x.PaymentLogs)
                .AsSplitQuery();
        }
    }
}