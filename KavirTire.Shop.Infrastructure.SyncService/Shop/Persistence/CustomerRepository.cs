using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence
{
    public class CustomerRepository : EfRepository<Customer>
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Customer GetLastUpdatedCustomer()
        {
            return _dbContext.Customer.OrderByDescending(x => x.CrmRowVersion).FirstOrDefault();
        }
    }
}