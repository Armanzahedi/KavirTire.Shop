using EasyCaching.Core;
using KavirTire.Shop.Application.Common.Cache;

namespace KavirTire.Shop.Infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IEasyCachingProvider _cacheProvider;

    public CacheService(IEasyCachingProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public void Flush()
    {
        _cacheProvider.Flush();
    }
    
    public async Task CacheAsync<T>(string key, T value)
    {
        await _cacheProvider.SetAsync(key,value,TimeSpan.FromDays(1));
    }
    public void Cache<T>(string key, T value)
    {
         _cacheProvider.Set(key,value,TimeSpan.FromDays(1));
    }
}