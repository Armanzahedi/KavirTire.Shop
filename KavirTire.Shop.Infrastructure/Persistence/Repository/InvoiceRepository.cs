using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace KavirTire.Shop.Infrastructure.Persistence.Repository;

public class InvoiceRepository : EfRepository<Invoice>, IInvoiceRepository
{
    private readonly AppDbContext _dbContext;

    public InvoiceRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteInvoiceItemsAsync(Guid invoiceId,CancellationToken cancellationToken = new())
    {
        await _dbContext.InvoiceItems.Where(x => x.InvoiceId == invoiceId).ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}