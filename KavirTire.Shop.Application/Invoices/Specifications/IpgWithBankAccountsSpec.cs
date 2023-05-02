using Ardalis.Specification;
using KavirTire.Shop.Domain.IPGs;

namespace KavirTire.Shop.Application.Invoices.Specifications;

public class IpgWithBankAccountsSpec : Specification<Ipg>
{
    public IpgWithBankAccountsSpec()
    {
        Query.OrderBy(x=>x.SequenceNumber).Include(x=>x.BankAccounts)
            .EnableCache(nameof(IpgWithBankAccountsSpec));
    }
}