using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Application.Common.Specifications;
using KavirTire.Shop.Domain.Customers;
using KavirTire.Shop.Domain.GeneralPolicy;
using KavirTire.Shop.Domain.Locations;
using MediatR;

namespace KavirTire.Shop.Application.Cart.Queries.GetCartInfo;

public record GetCartInfoQuery : IRequest<GetCartInfoQueryResult>;

public class GetCartInfoQueryHandler : IRequestHandler<GetCartInfoQuery, GetCartInfoQueryResult>
{
    private readonly GeneralPolicyService _generalPolicyService;
    private readonly ICurrentUser _currentUser;
    private readonly IReadRepository<Customer> _customerRepo;
    private readonly IReadRepository<Location> _locationRepo;

    public GetCartInfoQueryHandler(
        ICurrentUser currentUser,
        IReadRepository<Customer> customerRepo,
        IReadRepository<Location> locationRepo, GeneralPolicyService generalPolicyService)
    {
        _currentUser = currentUser;
        _customerRepo = customerRepo;
        _locationRepo = locationRepo;
        _generalPolicyService = generalPolicyService;
    }

    public async Task<GetCartInfoQueryResult> Handle(GetCartInfoQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(_currentUser.UserId, "Current User Id");
        var generalPolicy = await _generalPolicyService.GetGeneralPolicy(cancellationToken);
        Guard.Against.Null(generalPolicy, nameof(generalPolicy));


        var customer = await _customerRepo.FirstOrDefaultAsync(new CustomerWithOrderHistoryByIdSpec(_currentUser.UserId.Value),
            cancellationToken);
        Guard.Against.Null(customer, nameof(customer));

        var tirePostCost = await GetTirePostCostForCustomer(customer, cancellationToken);


        return new GetCartInfoQueryResult
        {
            MaximumNumberOfPurchases = generalPolicy.MaximumNumberOfPurchases,
            NumberOfPurchaseItems = generalPolicy.NumberOfPurchaseItems,
            ApplyMaximumNumberOfPurchases = generalPolicy.ApplyMaximumNumberOfPurchases,
            ApplyPurchaseInterval = generalPolicy.ApplyPurchaseInterval,
            ApplyNumberOfPurchaseItems = generalPolicy.ApplyNumberOfPurchaseItems,
            CustomerPreviousPurchaseCountInPurchaseInterval = customer.PreviousPurchaseCount(),
            TirePostCost = tirePostCost
        };
    }

    private async Task<decimal> GetTirePostCostForCustomer(Customer customer, CancellationToken cancellationToken)
    {
        decimal tirePostCost = 0;
        if (customer.ProvinceId != null)
        {
            var location =
                await _locationRepo.FirstOrDefaultAsync(new LocationWithPostCostCategoryByIdSpec(customer.ProvinceId.Value),
                    cancellationToken);
            tirePostCost = location?.GetPostCost() ?? 0;
        }

        return tirePostCost;
    }
}