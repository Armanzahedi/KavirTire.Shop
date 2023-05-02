using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.PriceLists.Entities;

namespace KavirTire.Shop.Domain.PriceLists;

public class PriceList : EntityBase<Guid>, IAggregateRoot
{
    public string? Name { get; set; }
    private readonly List<PriceListItem> _priceListItems = new();
    public IEnumerable<PriceListItem> PriceListItems => _priceListItems.AsReadOnly();
}