namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository
{
    public interface IRepositoryFactory
    {
        T CreateRepository<T>() where T : IRepository;
    }
}
