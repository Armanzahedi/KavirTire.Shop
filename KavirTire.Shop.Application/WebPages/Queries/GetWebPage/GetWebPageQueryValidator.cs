using FluentValidation;

namespace KavirTire.Shop.Application.WebPages.Queries.GetWebPage;

public class GetWebPageQueryValidator : AbstractValidator<GetWebPageQuery>
{
    public GetWebPageQueryValidator()
    {
        RuleFor(x => x.Key).NotNull().NotEmpty();
    }
}