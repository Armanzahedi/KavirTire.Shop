using Ardalis.Specification;
using KavirTire.Shop.Domain.WebPages;

namespace KavirTire.Shop.Application.WebPages.Specifications;

public class WebPageByKeySpec : Specification<WebPage>
{
    public WebPageByKeySpec(string key)
    {
        Query.Where(x => x.Key.ToLower().Trim() == key.ToLower().Trim())
            .EnableCache($"{nameof(WebPageByKeySpec)}-{key}");
    }
}