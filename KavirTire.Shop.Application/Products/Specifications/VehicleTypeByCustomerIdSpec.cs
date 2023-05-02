using Ardalis.Specification;
using KavirTire.Shop.Domain.Vehicles;
using KavirTire.Shop.Domain.VehicleTypes;

namespace KavirTire.Shop.Application.Products.Specifications;

// public class VehicleTypeByCustomerIdSpec : Specification<VehicleType>
// {
//     public VehicleTypeByCustomerIdSpec(Guid customerId)
//     {
//         Query
//             .Where(x => x.CustomerId == customerId && x.VehicleType != null)
//             .EnableCache($"{nameof(VehicleTypeByCustomerIdSpec)}-{customerId}");
//     }
// }