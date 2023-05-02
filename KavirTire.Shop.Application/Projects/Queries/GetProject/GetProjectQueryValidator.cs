using FluentValidation;

namespace KavirTire.Shop.Application.Projects.Queries.GetProject;

public class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
{
    public GetProjectQueryValidator()
    {
        RuleFor(x => x.projectId).NotEmpty();
    }   
}