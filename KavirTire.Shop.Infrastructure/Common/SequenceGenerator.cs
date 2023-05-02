using EasyCaching.Core;
using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Infrastructure.Cache;

namespace KavirTire.Shop.Infrastructure.Common;

public class SequenceGenerator : ISequenceGenerator
{
    private readonly IEasyCachingProvider _cachingProvider;
    private readonly IDistributedLock _distributedLock;
    private const string SequenceKey = "sequence";

    public SequenceGenerator(IEasyCachingProvider cachingProvider,
        IDistributedLock distributedLock)
    {
        _cachingProvider = cachingProvider;
        _distributedLock = distributedLock;
    }

    public async Task<int> GetNext(CancellationToken cancellationToken = new())
    {
        return await _distributedLock.Lock(SequenceKey, async () =>
        {
            var seq = await _cachingProvider.GetOrCreate<int>(SequenceKey, () => 1235);
            seq += 1;
            await _cachingProvider.SetAsync<int>(SequenceKey, seq, TimeSpan.FromDays(1), cancellationToken);
            return seq;
        }, cancellationToken);
    }
    public async Task<int> GetNext(string key,CancellationToken cancellationToken = new())
    {
        return await _distributedLock.Lock($"{key}-{SequenceKey}", async () =>
        {
            var seq = await _cachingProvider.GetOrCreate<int>($"{key}-{SequenceKey}", () => 1235);
            seq += 1;
            await _cachingProvider.SetAsync<int>($"{key}-{SequenceKey}", seq, TimeSpan.FromDays(1), cancellationToken);
            return seq;
        }, cancellationToken);
    }
}