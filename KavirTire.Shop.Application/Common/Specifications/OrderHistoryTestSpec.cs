using Ardalis.Specification;
using KavirTire.Shop.Domain.OrderHistory;

namespace KavirTire.Shop.Application.Common.Specifications;

public class OrderHistoryTestSpec : Specification<OrderHistory>
{
    public OrderHistoryTestSpec()
    {
        var from = DateTime.Now.AddDays(-1).ToUniversalTime();
        var to = DateTime.Now.ToUniversalTime();
        Query
            .Where(x => x
                .RegistrationDate != null && 
                        x.RegistrationDate > from.Date &&
                        x.RegistrationDate < to.Date);
    }
}