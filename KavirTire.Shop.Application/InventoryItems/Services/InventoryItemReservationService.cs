using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Domain.InventoryItems;

namespace KavirTire.Shop.Application.InventoryItems.Services;

public class InventoryItemReservationService
{
    private readonly IDistributedLock _distributedLock;

    private readonly IRepository<InventoryItem> _inventoryItemRepo;

    public InventoryItemReservationService(IDistributedLock distributedLock,
        IRepository<InventoryItem> inventoryItemRepo)
    {
        _distributedLock = distributedLock;
        _inventoryItemRepo = inventoryItemRepo;
    }

    public async Task Reserve(Guid inventoryItemId, int quantity,CancellationToken cancellationToken = new())
    {
        await _distributedLock
            .Lock($"InventoryItem-{inventoryItemId}", async () =>
            {
                var inventoryItem = await _inventoryItemRepo.GetByIdAsync(inventoryItemId, cancellationToken);
                inventoryItem!.ReserveProduct(quantity);
                await _inventoryItemRepo.UpdateAsync(inventoryItem, cancellationToken);
            }, cancellationToken);
        
    }
    public async Task ReleaseFromReserve(Guid inventoryItemId, int quantity,CancellationToken cancellationToken = new())
    {
        await _distributedLock
            .Lock($"InventoryItem-{inventoryItemId}", async () =>
            {
                var inventoryItem = await _inventoryItemRepo.GetByIdAsync(inventoryItemId, cancellationToken);
                inventoryItem!.ReleaseFromReserve(quantity);
                await _inventoryItemRepo.UpdateAsync(inventoryItem, cancellationToken);
            }, cancellationToken);
    }

    public async Task RemoveFromInventory(Guid inventoryItemId, int quantity,CancellationToken cancellationToken = new())
    {
        await _distributedLock
            .Lock($"InventoryItem-{inventoryItemId}", async () =>
            {
                var inventoryItem = await _inventoryItemRepo.GetByIdAsync(inventoryItemId, cancellationToken);
                inventoryItem!.RemoveFromInventory(quantity);
                await _inventoryItemRepo.UpdateAsync(inventoryItem, cancellationToken);
            }, cancellationToken);
    }
}