using Ardalis.Specification;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.Enums;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.Specifications
{
    public class OutOfSyncInventoryItemSpec : Specification<InventoryItem>
    {
        public OutOfSyncInventoryItemSpec()
        {
            Query.Where(x => x.SyncStatus == SyncStatus.OutOfSync);
        }
    }
}