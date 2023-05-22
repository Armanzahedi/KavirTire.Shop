using Ardalis.Specification;
using KavirTire.Shop.Domain.Customers;

namespace KavirTire.Shop.Application.Common.Specifications;

public class CustomerIsApprovedForPurchaseSpec : Specification<Customer,bool>
{
    public CustomerIsApprovedForPurchaseSpec(Guid? customerId)
    {
        Query
            .Select(x => x.IsApprovedForPurchase)
            .Where(x=>x.Id == customerId);
    }
}