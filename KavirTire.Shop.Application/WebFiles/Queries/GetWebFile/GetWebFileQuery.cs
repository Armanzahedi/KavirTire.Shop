using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Application.WebFiles.Specifications;
using KavirTire.Shop.Domain.WebFiles;
using MediatR;

namespace KavirTire.Shop.Application.WebFiles.Queries.GetWebFile;

public record GetWebFileQuery(string PartialUrl) : IRequest<WebFile?>;

public class GetWebFileQueryHandler : IRequestHandler<GetWebFileQuery,WebFile?>
{
    private readonly IReadRepository<WebFile> _webFileRepo;

    public GetWebFileQueryHandler(IReadRepository<WebFile> webFileRepo)
    {
        _webFileRepo = webFileRepo;
    }

    public async Task<WebFile?> Handle(GetWebFileQuery request, CancellationToken cancellationToken)
    {
        var webFile = await _webFileRepo.FirstOrDefaultAsync(new WebFileByPartialUrlSpec(request.PartialUrl), cancellationToken);
        return  webFile;
    }
}