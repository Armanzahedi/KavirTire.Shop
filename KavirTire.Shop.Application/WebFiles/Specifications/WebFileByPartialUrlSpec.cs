using Ardalis.Specification;
using KavirTire.Shop.Domain.WebFiles;

namespace KavirTire.Shop.Application.WebFiles.Specifications;

public sealed class WebFileByPartialUrlSpec : Specification<WebFile>
{
    public WebFileByPartialUrlSpec(string partialUrl)
    {
        Query.Where(a => a.PartialUrl.ToLower() == partialUrl)
            .EnableCache($"{nameof(WebFileByPartialUrlSpec)}-{partialUrl}");
    }
}