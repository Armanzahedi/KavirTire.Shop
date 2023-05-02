namespace KavirTire.Shop.Application.Common;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    Task SaveAndCommitAsync(CancellationToken cancellationToken = new());
}
