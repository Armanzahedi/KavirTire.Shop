using System;
using System.Configuration;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config
{
    public class CrmSettingsManager : ICrmSettingManager
    {
        #region ISettingManager

        public T GetSettings<T>() where T : ISetting
        {
            if (typeof(T) == typeof(GeneralSettings))
            {
                return (T)GetGeneralSettings();
            }

            throw new Exception("Invalid Setting Type");
        }

        #endregion

        private ISetting GetGeneralSettings()
        {
            var setting = new GeneralSettings
            {
                Domain = ConfigurationManager.AppSettings["CRM_DOMAIN"] ?? "",
                UserName = ConfigurationManager.AppSettings["CRM_USERNAME"],
                Password = ConfigurationManager.AppSettings["CRM_PASSWORD"],
                OrganizationServiceAddress = ConfigurationManager.AppSettings["CRM_ORGANIZATION_SERVICE_ADDRESS"]
            };

            return setting;
        }
    }
}