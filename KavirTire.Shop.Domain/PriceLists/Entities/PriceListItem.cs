using KavirTire.Shop.Domain.Common;

namespace KavirTire.Shop.Domain.PriceLists.Entities;

public class PriceListItem : EntityBase<Guid>
{
    public Guid? ProductId { get; set; }
    
    public Guid? PriceListId { get; set; }
    
    public decimal Amount { get; set; }
}