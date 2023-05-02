using Ardalis.Specification;
using KavirTire.Shop.Domain.InventoryItems;

namespace KavirTire.Shop.Application.Payments.Specifications;

public class InventoryItemIdByProductIdSpec : Specification<InventoryItem,Guid>
{
    public InventoryItemIdByProductIdSpec(Guid productId)
    {
        Query
            .Select(x=>x.Id)
            .Where(x => x.ProductId == productId);
    }
}