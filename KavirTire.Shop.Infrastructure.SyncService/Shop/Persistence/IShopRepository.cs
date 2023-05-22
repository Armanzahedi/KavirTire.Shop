using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence
{
    public interface IShopRepository<T> : IRepositoryBase<T> where T : class
    {
        void AddOrUpdate(T entity);
        Task AddOrUpdateAsync(T entity);
        Task AddOrUpdateRangeAsync(IEnumerable<T> entity);
        
        /// <summary>Deletes All af the records in the database that are not in the passed in list of ids.</summary>
        Task DeleteObsoleteRecords(List<Guid> ignoreIds);
        
        /// <summary>Deletes All af the records.</summary>
        Task DeleteObsoleteRecords();

    }

    public interface IShopReadRepository<T> : IReadRepositoryBase<T> where T : class
    {

    }
}