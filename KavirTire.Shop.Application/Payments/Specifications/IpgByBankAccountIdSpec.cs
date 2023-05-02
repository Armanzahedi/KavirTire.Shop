using Ardalis.Specification;
using KavirTire.Shop.Domain.IPGs;

namespace KavirTire.Shop.Application.Payments.Specifications;

public class IpgByBankAccountIdSpec : Specification<Ipg>
{
    public IpgByBankAccountIdSpec(Guid bankAccountId)
    {
        Query
            .AsNoTracking()
            .Include(x=>x.BankAccounts)
            .Where(x => x.BankAccounts.Any(b=>b.Id == bankAccountId))
            .EnableCache($"{nameof(IpgByBankAccountIdSpec)}-{bankAccountId}");
    }
}