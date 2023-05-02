using EasyCaching.Core;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.Extensions.DependencyInjection;

namespace KavirTire.Shop.Infrastructure.Cache;

public static class EasyCachingExtensions
{
    public static async Task<TItem> GetOrCreate<TItem>(this IEasyCachingProvider cacheProvider, string key,
        Func<TItem> factory)
    {
        var data = await cacheProvider.GetAsync<TItem>(key);
        if (data.HasValue) return data.Value;
        
        var dataToCache = factory();
        await cacheProvider.SetAsync<TItem>(key, dataToCache, TimeSpan.FromDays(1));
        return dataToCache;
    }
    public static async Task<TItem> GetOrCreateAsync<TItem>(this IEasyCachingProvider cacheProvider, string key,
        Func<Task<TItem>> factory)
    {
        var data = await cacheProvider.GetAsync<TItem>(key);
        if (data.HasValue) return data.Value;
        
        var dataToCache = await factory().ConfigureAwait(false);
        await cacheProvider.SetAsync<TItem>(key, dataToCache, TimeSpan.FromDays(1));
        return dataToCache;
    }
    public static async Task FlushCache(this IServiceScope serviceScope)
    {
        var cacheProvider = serviceScope.ServiceProvider.GetRequiredService<IEasyCachingProvider>();
        await cacheProvider.FlushAsync();
    }
}