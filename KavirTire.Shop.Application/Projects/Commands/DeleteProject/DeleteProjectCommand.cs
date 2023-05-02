using MediatR;
using KavirTire.Shop.Domain.Project;
using KavirTire.Shop.Application.Common.Exceptions;
using KavirTire.Shop.Application.Common.Persistence;

namespace KavirTire.Shop.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid projectId) : IRequest;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IRepository<Project> _projectRepository;

    public DeleteProjectCommandHandler(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.projectId, cancellationToken);
        if (project == null) throw new NotFoundException();
        
        await _projectRepository.DeleteAsync(project, cancellationToken);
    }
}