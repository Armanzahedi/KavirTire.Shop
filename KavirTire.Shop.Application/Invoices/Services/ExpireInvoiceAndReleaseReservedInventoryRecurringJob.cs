using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.RecurringJob;
using KavirTire.Shop.Application.InventoryItems.Services;
using KavirTire.Shop.Application.Invoices.Specifications;
using KavirTire.Shop.Application.Payments.Specifications;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.Invoices.Enums;

namespace KavirTire.Shop.Application.Invoices.Services;


[CronSchedule("* * * * *")]
public class ExpireInvoiceAndReleaseReservedInventoryRecurringJob : IRecurringJob
{
    private readonly IInvoiceRepository _invoiceReadRepo;
    private readonly IRepository<Invoice> _invoiceRepo;
    private readonly IDistributedLock _distributedLock;
    private readonly InventoryItemReservationService _inventoryItemReservationService;

    private readonly IReadRepository<InventoryItem> _inventoryItemReadRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ExpireInvoiceAndReleaseReservedInventoryRecurringJob(
        IInvoiceRepository invoiceReadRepo,
        IReadRepository<InventoryItem> inventoryItemReadRepo,
        IRepository<Invoice> invoiceRepo,
        IUnitOfWork unitOfWork,
        InventoryItemReservationService inventoryItemReservationService,
        IDistributedLock distributedLock)
    {
        _invoiceReadRepo = invoiceReadRepo;
        _inventoryItemReadRepo = inventoryItemReadRepo;
        _invoiceRepo = invoiceRepo;
        _unitOfWork = unitOfWork;
        _inventoryItemReservationService = inventoryItemReservationService;
        _distributedLock = distributedLock;
    }

    public async Task Run()
    {
        var expiredInvoices = await _invoiceReadRepo.ListAsync(new ExpiredInvoiceWithItemsSpec());

        foreach (var invoice in expiredInvoices)
        {
            await _distributedLock.Lock($"Invoice-{invoice.Id}", async () =>
            {
                _unitOfWork.BeginTransaction();
                invoice.InvoiceStatus = InvoiceStatus.Expired;
                foreach (var invoiceItem in invoice.InvoiceItems)
                {
                    var inventoryItemId = await _inventoryItemReadRepo
                        .FirstOrDefaultAsync(new InventoryItemIdByProductIdSpec(invoiceItem.ProductId));
                    await _inventoryItemReservationService.ReleaseFromReserve(inventoryItemId ,invoiceItem.Quantity);
                }

                await _invoiceRepo.UpdateAsync(invoice);
                _unitOfWork.Commit();
            });
        }
    }
}