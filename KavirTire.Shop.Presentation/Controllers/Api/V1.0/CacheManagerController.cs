using KavirTire.Shop.Application.Common.Cache;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;

namespace KavirTire.Shop.Presentation.Controllers.Api.V1._0;

public class CacheManagerController : ApiControllerBase
{
    private readonly ICacheService _cacheService;
    
    public CacheManagerController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpPost("Reset")]
    public IActionResult ResetCache()
    {
        _cacheService.Flush();
        return Ok();
    }
}