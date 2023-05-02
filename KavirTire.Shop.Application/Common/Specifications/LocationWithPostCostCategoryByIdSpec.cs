using Ardalis.Specification;
using KavirTire.Shop.Domain.Locations;

namespace KavirTire.Shop.Application.Common.Specifications;

public class LocationWithPostCostCategoryByIdSpec : Specification<Location>
{
    public LocationWithPostCostCategoryByIdSpec(Guid locationId)
    {
        Query.Where(l => l.Id == locationId).Include(l => l.PostCostCategory)
            .EnableCache($"{nameof(LocationWithPostCostCategoryByIdSpec)}-{locationId}");
    }
}