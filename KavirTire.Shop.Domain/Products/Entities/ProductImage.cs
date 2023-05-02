using KavirTire.Shop.Domain.Common;

namespace KavirTire.Shop.Domain.Products.Entities;

public class ProductImage : EntityBase<Guid>
{
    public Guid ProductId { get; set; }
    public string ImageUrl { get; set; }
}