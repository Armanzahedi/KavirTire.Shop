using KavirTire.Shop.Application.Common.Specifications.PaginatedList;
using Microsoft.AspNetCore.Mvc;
using KavirTire.Shop.Application.Projects;
using KavirTire.Shop.Application.Projects.Commands.AddTodo;
using KavirTire.Shop.Application.Projects.Commands.CreateProject;
using KavirTire.Shop.Application.Projects.Commands.DeleteProject;
using KavirTire.Shop.Application.Projects.Commands.UpdateProject;
using KavirTire.Shop.Application.Projects.Queries.GetProject;
using KavirTire.Shop.Application.Projects.Queries.GetProjects;
using KavirTire.Shop.Presentation.Models.Projects;

namespace KavirTire.Shop.Presentation.Controllers.Api.V1._0;

public class ProjectsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult> AddProject([FromBody] CreateProjectCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction("GetProject", new { projectId = result.Id },result);
    }
    
    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectResult>> GetProject(GetProjectQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProjectResult>>> GetProjects([FromQuery] GetProjectsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProject(Guid projectId, [FromBody] UpdateProjectRequest request)
    {
        await Mediator.Send(new UpdateProjectCommand(projectId,request.description));
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteProject(Guid projectId)
    {
        await Mediator.Send(new DeleteProjectCommand(projectId));
        return NoContent();
    }
    
    [HttpPost("{projectId}/todos")]
    public async Task<ActionResult> AddTodo(Guid projectId, [FromBody] AddTodoRequest request)
    {
        await Mediator.Send(new AddTodoCommand(projectId,request.title));
        return NoContent();
    }
}