using KavirTire.Shop.Application.Common;
using Microsoft.Extensions.Options;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace KavirTire.Shop.Infrastructure.Common;

public class DistributedLock : IDistributedLock
{
    private readonly RedisOptions _redisOptions;
    private readonly DistributedLockOptions _distributedLockOptions;

    public DistributedLock(IOptions<RedisOptions> redisOptions, IOptions<DistributedLockOptions> distributedLockOptions)
    {
        _redisOptions = redisOptions.Value;
        _distributedLockOptions = distributedLockOptions.Value;
    }

    public async Task Lock(string resource, Func<Task> action,CancellationToken cancellationToken = new())
    {
        var existingConnectionMultiplexer = await ConnectionMultiplexer.ConnectAsync($"{_redisOptions.Address}:{_redisOptions.Port}");

        var multiplexers = new List<RedLockMultiplexer> { existingConnectionMultiplexer };
        var redLockFactory = RedLockFactory.Create(multiplexers);
        
        var expiry = TimeSpan.FromSeconds(Convert.ToInt32(_distributedLockOptions.Expiry));
        var wait = TimeSpan.FromSeconds(Convert.ToInt32(_distributedLockOptions.Wait));
        var retry = TimeSpan.FromSeconds(Convert.ToInt32(_distributedLockOptions.Retry));
        
        await using var redLock = await redLockFactory.CreateLockAsync(resource, expiry, wait, retry);
        if (redLock.IsAcquired)
        {
            await action();
        }
    }
    public async Task<T?> Lock<T>(string resource, Func<Task<T?>> action,CancellationToken cancellationToken = new())
    {
        var existingConnectionMultiplexer = await ConnectionMultiplexer.ConnectAsync($"{_redisOptions.Address}:{_redisOptions.Port}");

        var multiplexers = new List<RedLockMultiplexer> { existingConnectionMultiplexer };
        var redLockFactory = RedLockFactory.Create(multiplexers);
        
        var expiry = TimeSpan.FromSeconds(Convert.ToInt32(_distributedLockOptions.Expiry));
        var wait = TimeSpan.FromSeconds(Convert.ToInt32(_distributedLockOptions.Wait));
        var retry = TimeSpan.FromSeconds(Convert.ToInt32(_distributedLockOptions.Retry));
        
        await using var redLock = await redLockFactory.CreateLockAsync(resource, expiry, wait, retry);
        T? result = default;
        if (redLock.IsAcquired)
        {
            result = await action();
        }
        
        return result;
    }
}