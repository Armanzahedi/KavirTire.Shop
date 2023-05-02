using Ardalis.Specification;
using KavirTire.Shop.Domain.Customers;

namespace KavirTire.Shop.Application.Common.Specifications;

public class CustomerWithOrderHistoryByIdSpec : Specification<Customer>
{
    public CustomerWithOrderHistoryByIdSpec(Guid customerId)
    {
        Query
            .Where(c => c.Id == customerId)
            .Include(c => c.OrderHistory)
            .AsNoTracking()
            .AsSplitQuery()
            .EnableCache($"{nameof(CustomerWithOrderHistoryByIdSpec)}-{customerId}");
    }
}