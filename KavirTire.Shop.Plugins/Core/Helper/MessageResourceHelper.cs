using System.Linq;
using System.Reflection;
using System.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Plugins.Core.Helper
{
    public class MessageResourceHelper
    {
        protected PluginBase.LocalPluginContext _ctx;
        protected IOrganizationService _service;
        private readonly Entity _userSetting;
        public MessageResourceHelper(PluginBase.LocalPluginContext ctx)
        {
            _service = ctx.OrganizationService;
            _ctx = ctx;
            _userSetting = this.GetCurrentUserSetting();
        }

        public string GetLocalized(string key)
        {
            var lcid = _userSetting.GetAttributeValue<int>("uilanguageid");

            var resourceName = "MessageResource";
            if (lcid == 1065)
            {
                resourceName += "_fa";
            }
            var resourceManager =
                new ResourceManager($"{Assembly.GetExecutingAssembly().GetName().Name}.Resources.{resourceName}",
                    Assembly.GetExecutingAssembly());

            return resourceManager.GetString(key);
        }
        private Entity GetCurrentUserSetting()
        {
            var fetchXml = $@"<fetch count='1' >
                         <entity name='usersettings' >
                           <attribute name='uilanguageid'/>  
                           <filter type='and' >
                             <condition attribute='systemuserid' operator='eq' value='{_ctx.PluginExecutionContext.InitiatingUserId}' />
                           </filter>
                           <link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' >
                             <attribute name='fullname' />
                             <link-entity name='organization' from='organizationid' to='organizationid' >
                               <attribute name='localeid' />
                             </link-entity>
                           </link-entity>
                         </entity>
                        </fetch>";

            var fetchXmlQuery = new FetchExpression(fetchXml);
            var resultCollection = _service.RetrieveMultiple(fetchXmlQuery);
            if (resultCollection != null && resultCollection.Entities.Count > 0)
            {
                return resultCollection.Entities.ToList().FirstOrDefault();
            }

            return null;
        }
    }
}
