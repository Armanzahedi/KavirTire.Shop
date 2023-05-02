using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Proxy
{
    class CrmServiceProxyFactory
    {
        private static IServiceManagement<IOrganizationService> serviceManagement;

        private ICrmSettingManager _crmSettingsManager;

        public CrmServiceProxyFactory(ICrmSettingManager currentCrmSettingManager)
        {
            _crmSettingsManager = currentCrmSettingManager;
        }

        public ManagedTokenOrganizationServiceProxy CreateInstance()
        {
            var setting = _crmSettingsManager.GetSettings<GeneralSettings>();

            var credentials = new ClientCredentials();
            if (serviceManagement == null)
            {
                serviceManagement = ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri(setting.OrganizationServiceAddress));
            }
            if (!string.IsNullOrEmpty(setting.Domain))
            {
                var networkCredential = new NetworkCredential(setting.UserName, setting.Password, setting.Domain);
                credentials.Windows.ClientCredential = networkCredential;

            }
            else
            {
                credentials.UserName.UserName = setting.UserName;
                credentials.UserName.Password = setting.Password;
            }

            credentials.SupportInteractive = false;
            var serviceProxy = new ManagedTokenOrganizationServiceProxy(serviceManagement, credentials);
            if (serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.EndpointBehaviors.Count == 0)
            {
                serviceProxy.EnableProxyTypes();
            }
            
            return serviceProxy;
        }
    }
}
