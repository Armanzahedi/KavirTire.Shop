using Ardalis.Specification;
using KavirTire.Shop.Domain.OrderHistory;

namespace KavirTire.Shop.Application.Cart.Specifications;

public class OrderHistoryByCustomerIdSpec : Specification<OrderHistory>
{
    public OrderHistoryByCustomerIdSpec(Guid customerId)
    {
        Query.Where(x => x.CustomerId == customerId);
    }

    public OrderHistoryByCustomerIdSpec(Guid customerId,int? periodInDay)
    {
        Query.Where(x => x.CustomerId == customerId);
        if (periodInDay != null)
        {
            var toDate = DateTime.Now;
            var fromDate = toDate.AddDays(periodInDay.Value * -1);
            Query.Where(x => x.RegistrationDate <= toDate && x.RegistrationDate >= fromDate)
                .EnableCache($"{nameof(OrderHistoryByCustomerIdSpec)}-{customerId}-{periodInDay}");
        }
    }
}