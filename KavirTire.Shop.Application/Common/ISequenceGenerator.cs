namespace KavirTire.Shop.Application.Common;

public interface ISequenceGenerator
{
    Task<int> GetNext(CancellationToken cancellationToken = new());
    Task<int> GetNext(string key, CancellationToken cancellationToken = new());
}