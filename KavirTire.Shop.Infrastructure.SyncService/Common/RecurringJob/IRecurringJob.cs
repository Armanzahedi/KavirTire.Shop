using System.Threading.Tasks;

namespace KavirTire.Shop.Infrastructure.SyncService.Common.RecurringJob
{
    public interface IRecurringJob
    {
        Task Run();
    }
}