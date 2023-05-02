using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.WebPages.Specifications;
using KavirTire.Shop.Domain.WebPages;
using MediatR;

namespace KavirTire.Shop.Application.WebPages.Queries.GetWebPage;

public record GetWebPageQuery(string Key): IRequest<WebPage?>;

public class GetWebPageQueryHandler : IRequestHandler<GetWebPageQuery, WebPage?>
{
    private readonly IReadRepository<WebPage> _webPageRepo;

    public GetWebPageQueryHandler(IReadRepository<WebPage> webPageRepo)
    {
        _webPageRepo = webPageRepo;
    }

    public async Task<WebPage?> Handle(GetWebPageQuery request, CancellationToken cancellationToken)
    {
        return await _webPageRepo.FirstOrDefaultAsync(new WebPageByKeySpec(request.Key), cancellationToken);
    }
}