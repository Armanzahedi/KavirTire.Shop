namespace KavirTire.Shop.Application.Common.Cache;

public interface ICacheService
{
    void Flush();
    Task CacheAsync<T>(string key, T value);
    void Cache<T>(string key, T value);
}