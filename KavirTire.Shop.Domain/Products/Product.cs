using Ardalis.GuardClauses;
using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Domain.PriceLists;
using KavirTire.Shop.Domain.PriceLists.Entities;
using KavirTire.Shop.Domain.Products.Entities;
using KavirTire.Shop.Domain.Products.Exceptions;
using KavirTire.Shop.Domain.VehicleTypes;
using KavirTire.Shop.Domain.VehicleTypes.Entities;
using KavirTire.Shop.Domain.VehicleTypes.Enums;

namespace KavirTire.Shop.Domain.Products;

public class Product : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; set; }

    private readonly List<VehicleTypeProduct> _vehicleTypeProducts = new();
    public IEnumerable<VehicleTypeProduct> VehicleTypeProducts => _vehicleTypeProducts.AsReadOnly();

    private readonly List<PriceListItem> _priceListItems = new();
    public IEnumerable<PriceListItem> PriceListItems => _priceListItems.AsReadOnly();

    private readonly List<InventoryItem> _inventoryItems = new();
    public IEnumerable<InventoryItem> InventoryItems => _inventoryItems.AsReadOnly();


    private readonly List<ProductImage?> _productImages = new();
    public IEnumerable<ProductImage?> ProductImages => _productImages.AsReadOnly();

    public ProductImage? MainImage => _productImages.FirstOrDefault();
    public int QuantityInStock => InventoryItems.FirstOrDefault()?.InventoryForSale ?? 0;

    public InventoryItem? GetInventory()
    {
        return _inventoryItems.FirstOrDefault();
    }
    public decimal GetPrice(Guid priceListId)
    {
        return _priceListItems.FirstOrDefault(x => x.PriceListId == priceListId)?.Amount ?? 0;
    }

    public ProductType? GetProductType(VehicleType? vehicleType)
    {
        if (vehicleType == null)
            return null;
        var type = _vehicleTypeProducts.FirstOrDefault(v => v.VehicleTypeId == vehicleType.Id)?.ProductType;
        Guard.Against.Null(type, nameof(type));

        return type.Value;
    }
}