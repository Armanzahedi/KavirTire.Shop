using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Products;
using KavirTire.Shop.Domain.VehicleTypes.Enums;

namespace KavirTire.Shop.Domain.VehicleTypes.Entities;

public class VehicleTypeProduct : EntityBase<Guid>
{
    public Guid ProductId { get; set; }
    
    public Guid VehicleTypeId { get; set; }
    public ProductType ProductType { get; set; }
}