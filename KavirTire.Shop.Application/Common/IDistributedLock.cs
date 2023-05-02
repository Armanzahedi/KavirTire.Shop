namespace KavirTire.Shop.Application.Common;

public interface IDistributedLock
{
    Task Lock(string resource, Func<Task> action, CancellationToken cancellationToken = new());
    Task<T?> Lock<T>(string resource, Func<Task<T?>> action, CancellationToken cancellationToken = new());
}