using Ardalis.Specification.EntityFrameworkCore;
using KavirTire.Shop.Application.Common.Persistence;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Infrastructure.Persistence.Common;

namespace KavirTire.Shop.Infrastructure.Persistence.Repository;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    private readonly AppDbContext _dbContext;
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}