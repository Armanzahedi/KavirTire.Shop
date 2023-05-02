using Ardalis.Specification;
using KavirTire.Shop.Domain.Vehicles;

namespace KavirTire.Shop.Application.Products.Specifications;

public class VehicleByCustomerId : Specification<Vehicle>
{
    public VehicleByCustomerId(Guid customerId)
    {
        Query.Where(x => x.CustomerId == customerId)
            .EnableCache($"{nameof(VehicleByCustomerId)}-{customerId}");
    }
}