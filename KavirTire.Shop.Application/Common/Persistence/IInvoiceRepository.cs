using KavirTire.Shop.Domain.Invoices;

namespace KavirTire.Shop.Application.Common.Persistence;

public interface IInvoiceRepository : IReadRepository<Invoice>, IRepository<Invoice>
{
    Task DeleteInvoiceItemsAsync(Guid invoiceId, CancellationToken cancellationToken = new());
}