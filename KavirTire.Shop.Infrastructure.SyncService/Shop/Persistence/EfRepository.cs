using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFramework6;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence
{
    public class EfRepository<T> : RepositoryBase<T>, IShopReadRepository<T>, IShopRepository<T>,IDisposable where T : EntityBase
    {
        private readonly AppDbContext _dbContext;
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrUpdate(T entity)
        {
            _dbContext.Set<T>().AddOrUpdate(a=>a.Id,entity);
        }

        public async  Task AddOrUpdateAsync(T entity)
        {
            _dbContext.Set<T>().AddOrUpdate(a=>a.Id,entity);
            await _dbContext.SaveChangesAsync();
        }

        public async  Task AddOrUpdateRangeAsync(IEnumerable<T> entity)
        {
            entity.ToList()?
                .ForEach(e=>_dbContext.Set<T>().AddOrUpdate(a=>a.Id,e));
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteObsoleteRecords(List<Guid> ignoreIds)
        {
            var query = $"DELETE FROM {EntityExtensions.GetTableName<T>()} ";
            if (ignoreIds?.Any() == true)
            {
                var ignoreIdsStr = ignoreIds.Select(a => $"'{a}'");
                query = query + $"WHERE Id NOT IN ({string.Join(",", ignoreIdsStr)})";
            }
            await _dbContext.Database.ExecuteSqlCommandAsync(query);
        }

        public async Task DeleteObsoleteRecords()
        {
            var query = $"DELETE FROM {EntityExtensions.GetTableName<T>()} ";
            await _dbContext.Database.ExecuteSqlCommandAsync(query);
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _dbContext?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EfRepository()
        {
            Dispose(false);
        }
    }
}