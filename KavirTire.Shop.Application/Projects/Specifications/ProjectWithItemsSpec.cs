using Ardalis.Specification;
using KavirTire.Shop.Domain.Project;

namespace KavirTire.Shop.Application.Projects.Specifications;

public class ProjectWithItemsSpec : Specification<Project>
{
    public ProjectWithItemsSpec()
    {
        Query.Include(x => x.Items);
    }
}