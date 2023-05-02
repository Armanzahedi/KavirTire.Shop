using Mapster;
using KavirTire.Shop.Application.Projects.Queries.GetProject;
using KavirTire.Shop.Domain.Project;

namespace KavirTire.Shop.Application.Projects;

public class ProjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, ProjectResult>()
            .Map(dest => dest.Title, src => src.Description)
            .Map(dest => dest.Todos, src => src.Items.Select(i => i.Title));
    }
}