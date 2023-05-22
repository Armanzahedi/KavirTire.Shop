using EasyCaching.Core;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Infrastructure.Cache;

namespace KavirTire.Shop.Infrastructure.Common;

public class SequenceGenerator
{
    private readonly IEasyCachingProvider _cachingProvider;
    private readonly IDistributedLock _distributedLock;
    private const string MainSequenceKey = "general";

    public SequenceGenerator(IEasyCachingProvider cachingProvider,
        IDistributedLock distributedLock)
    {
        _cachingProvider = cachingProvider;
        _distributedLock = distributedLock;
    }

    public async Task<int> GetNext(string key,int startFrom, CancellationToken cancellationToken = new())
    {
        return await _distributedLock.Lock($"{key}-seq", async () =>
        {
            var seq = await _cachingProvider.GetOrCreateAsync<int>($"{key}-seq", () => startFrom);
            seq += 1;
            await _cachingProvider.SetAsync<int>($"{key}-seq", seq, TimeSpan.FromDays(1), cancellationToken);
            return seq;
        }, cancellationToken);
        
    }
    public async Task<int> GetNext(int startFrom, CancellationToken cancellationToken = new())
    {
        return await GetNext(MainSequenceKey, startFrom, cancellationToken);
    }
    public async Task<int> GetNext(CancellationToken cancellationToken = new())
    {
        return await GetNext(1, cancellationToken);
    }
}