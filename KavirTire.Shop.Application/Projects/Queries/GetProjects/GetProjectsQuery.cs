using KavirTire.Shop.Application.Common.Persistence;
using MapsterMapper;
using MediatR;
using KavirTire.Shop.Application.Projects.Queries.GetProject;
using KavirTire.Shop.Domain.Project;
using KavirTire.Shop.Application.Common.Specifications.PaginatedList;
using KavirTire.Shop.Application.Projects.Specifications;

namespace KavirTire.Shop.Application.Projects.Queries.GetProjects;

public record GetProjectsQuery(int? pageNumber, int? pageSize) : IRequest<PaginatedList<ProjectResult>>;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectResult>>
{
    private readonly IReadRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IReadRepository<Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectResult>> Handle(GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var result =
            await _projectRepository
                .PaginatedListAsync<Project, ProjectResult>(new ProjectWithItemsSpec(),
                    request.pageNumber,
                    request.pageSize,
                    cancellationToken);
        return result;
    }
}