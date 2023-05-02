using KavirTire.Shop.Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using KavirTire.Shop.Domain.Project;

namespace KavirTire.Shop.Application.Projects.Commands.UpdateProject;

public record UpdateProjectCommand(Guid projectId, string description) : IRequest;

public record UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IRepository<Project> _projectRepository;

    public UpdateProjectCommandHandler(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.projectId, cancellationToken);
        project.Description = request.description;
        
        await _projectRepository.UpdateAsync(project);
    }
}