using KavirTire.Shop.Domain.VehicleTypes.Enums;

namespace KavirTire.Shop.Application.Products.Queries.GetProducts;

public record GetProductsQueryResult()
{
    public List<ProductsQueryResultProduct>? Products;
};
public record ProductsQueryResultProduct(Guid Id,string Name,decimal Price,int QuantityInStock,ProductType? ProductType, string? ImageUrl = "/not-found.png");