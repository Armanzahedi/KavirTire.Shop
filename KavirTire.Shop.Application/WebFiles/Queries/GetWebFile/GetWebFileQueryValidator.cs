using FluentValidation;

namespace KavirTire.Shop.Application.WebFiles.Queries.GetWebFile;

public class GetWebFileQueryValidator : AbstractValidator<GetWebFileQuery>
{
    public GetWebFileQueryValidator()
    {
        RuleFor(x => x.PartialUrl).NotNull().NotEmpty();
    }
}