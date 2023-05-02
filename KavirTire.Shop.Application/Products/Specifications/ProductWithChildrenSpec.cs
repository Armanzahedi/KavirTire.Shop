using Ardalis.Specification;
using KavirTire.Shop.Domain.Products;

namespace KavirTire.Shop.Application.Products.Specifications;

public class ProductWithChildrenSpec : Specification<Product>
{
    public ProductWithChildrenSpec()
    {
        Query
            .Include(x=>x.VehicleTypeProducts)
            .Include(x=>x.InventoryItems)
            .Include(x=>x.PriceListItems)
            .Include(x=>x.ProductImages)
            .AsSplitQuery()
            .EnableCache(nameof(ProductWithChildrenSpec));
    }
}