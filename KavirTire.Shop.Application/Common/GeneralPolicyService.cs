using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Domain.GeneralPolicy;

namespace KavirTire.Shop.Application.Common;

public class GeneralPolicyService
{
    private readonly IReadRepository<GeneralPolicy> _generalPolicyRepo;

    public GeneralPolicyService(IReadRepository<GeneralPolicy> generalPolicyRepo)
    {
        _generalPolicyRepo = generalPolicyRepo;
    }

    public async Task<GeneralPolicy?> GetGeneralPolicy(CancellationToken cancellationToken = new())
    {
        return (await _generalPolicyRepo.ListAsync(cancellationToken)).FirstOrDefault();
    }
}