using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config
{
    public class GeneralSettings : ISetting
    {
        private string password;
        public string UserName { get; set; }
        public string Password
        {
            get
            {
                var cryptoHelper = new CryptographyHelper();
                return cryptoHelper.DecryptString(password);
            }
            set { password = value; }
        }
        public string OrganizationServiceAddress { get; set; }
        public string Domain { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
    }
}
