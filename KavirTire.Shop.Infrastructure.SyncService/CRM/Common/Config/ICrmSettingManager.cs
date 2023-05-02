namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config
{
    public interface ICrmSettingManager
    {
        T GetSettings<T>() where T : ISetting;
    }
}
