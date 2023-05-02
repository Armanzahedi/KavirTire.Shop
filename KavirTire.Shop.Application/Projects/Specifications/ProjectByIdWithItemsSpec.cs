using Ardalis.Specification;
using KavirTire.Shop.Domain.Project;

namespace KavirTire.Shop.Application.Projects.Specifications;

public sealed class ProjectByIdWithItemsSpec : Specification<Project>
{
    public ProjectByIdWithItemsSpec(Guid projectId)
    {
        Query.Where(p => p.Id == projectId)
            .Include(p => p.Items);
    }
}