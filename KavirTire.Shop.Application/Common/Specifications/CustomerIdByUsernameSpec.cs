using Ardalis.Specification;
using KavirTire.Shop.Domain.Customers;

namespace KavirTire.Shop.Application.Common.Specifications;

public class CustomerIdByUsernameSpec : Specification<Customer,Guid>
{
    public CustomerIdByUsernameSpec(string username)
    {
        Query
            .Select(x => x.Id)
            .Where(x => x.Username == username);
    }
}