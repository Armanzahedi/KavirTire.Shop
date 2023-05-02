using KavirTire.Shop.Application.Common;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace KavirTire.Shop.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            return;
        }

        _transaction = _context.Database.BeginTransaction();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Commit()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Commit();
        _transaction.Dispose();
        _transaction = null;
    }

    public async Task SaveAndCommitAsync(CancellationToken cancellationToken = new())
    {
        await SaveChangesAsync(cancellationToken);
        Commit();
    }

    public void Rollback()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public void Dispose()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Dispose();
        _transaction = null;
    }
}