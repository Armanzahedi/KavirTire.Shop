using Ardalis.GuardClauses;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.Common.Services;
using KavirTire.Shop.Application.Products.Specifications;
using KavirTire.Shop.Domain.Products;
using KavirTire.Shop.Domain.Vehicles;
using KavirTire.Shop.Domain.VehicleTypes;
using MediatR;

namespace KavirTire.Shop.Application.Products.Queries.GetProducts;

public record GetProductsQuery() : IRequest<GetProductsQueryResult?>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResult?>
{
    private readonly ICurrentUser _currentUser;
    private readonly GeneralPolicyService _generalPolicyService;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IReadRepository<Vehicle> _vehicleRepo;
    private readonly IReadRepository<VehicleType> _vehicleTypeRepo;

    public GetProductsQueryHandler(ICurrentUser currentUser,
        IReadRepository<Product> productRepo,
        IReadRepository<Vehicle> vehicleRepo,
        IReadRepository<VehicleType> vehicleTypeRepo, GeneralPolicyService generalPolicyService)
    {
        _currentUser = currentUser;
        _productRepo = productRepo;
        _vehicleRepo = vehicleRepo;
        _vehicleTypeRepo = vehicleTypeRepo;
        _generalPolicyService = generalPolicyService;
    }

    public async Task<GetProductsQueryResult?> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepo.ListAsync(new ProductWithChildrenSpec(), cancellationToken);
        var generalPolicy = await _generalPolicyService.GetGeneralPolicy(cancellationToken);
        Guard.Against.Null(generalPolicy, nameof(generalPolicy));

        Vehicle? vehicle;
        VehicleType? vehicleType = null;
        if (generalPolicy.ShowProductsOnlyRelatedToCustomerCar)
        {   
            Guard.Against.Null(_currentUser.UserId, "Current UserId");

            vehicle = await _vehicleRepo.FirstOrDefaultAsync(new VehicleByCustomerId(_currentUser.UserId.Value), cancellationToken);

            if (vehicle?.VehicleTypeId != null)
            {
                vehicleType = await _vehicleTypeRepo.GetByIdAsync(vehicle.VehicleTypeId.Value, cancellationToken);
                products = products.Where(x => x.VehicleTypeProducts.Any(v => v.VehicleTypeId == vehicle.VehicleTypeId)).ToList();
            }
            else
                products = new List<Product>();

        }

        return new GetProductsQueryResult
        {
            Products = products.Select(x => new ProductsQueryResultProduct(x.Id,
                x.Name,
                x.GetPrice(generalPolicy.PriceListId!.Value),
                x.QuantityInStock,
                x.GetProductType(vehicleType),
                x.MainImage?.ImageUrl
            )).ToList()
        };
    }
}