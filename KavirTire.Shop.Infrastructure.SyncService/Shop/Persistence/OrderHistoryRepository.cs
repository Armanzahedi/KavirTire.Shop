using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence
{
    public class OrderHistoryRepository : EfRepository<OrderHistory>
    {
        private readonly AppDbContext _dbContext;

        public OrderHistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public OrderHistory GetLastUpdatedOrderHistory()
        {
            return _dbContext.OrderHistory.OrderByDescending(x => x.CrmRowVersion).FirstOrDefault();
        }
    }
}
