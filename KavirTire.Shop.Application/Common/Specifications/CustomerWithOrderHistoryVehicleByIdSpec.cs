using Ardalis.Specification;
using KavirTire.Shop.Domain.Customers;

namespace KavirTire.Shop.Application.Common.Specifications;

public class CustomerWithOrderHistoryVehicleByIdSpec : Specification<Customer>
{
    public CustomerWithOrderHistoryVehicleByIdSpec(Guid customerId)
    {
        Query.Where(c => c.Id == customerId)
            .Include(c => c.OrderHistory)
            .Include(c=>c.Vehicles)
            .EnableCache($"{nameof(CustomerWithOrderHistoryVehicleByIdSpec)}-{customerId}");
    }
}