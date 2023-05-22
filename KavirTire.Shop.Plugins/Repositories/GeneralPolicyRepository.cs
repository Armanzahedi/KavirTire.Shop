using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.KavirTire.Shop.Plugins.Entities;
using KavirTire.Shop.Plugins.Core;
using KavirTire.Shop.Plugins.Core.Repository;
using Microsoft.Xrm.Sdk.Query;
using System.Linq;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Repositories
{
    public class GeneralPolicyRepository : PluginRepository<GeneralPolicy>
    {
        private readonly PluginBase.LocalPluginContext _currentContext;
        public GeneralPolicyRepository(PluginBase.LocalPluginContext currentContext) : base(currentContext)
        {
            _currentContext = currentContext;
        }

        public GeneralPolicy GetGeneralPolicy()
        {
            var query = new QueryExpression(PluginResource.GeneralPolicy)
            {
                ColumnSet = new ColumnSet(
                    PluginResource.GeneralPolicy_SyncServiceAddress
                ),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(PluginResource.Status, ConditionOperator.Equal, 0),
                        new ConditionExpression(PluginResource.StatusReason, ConditionOperator.Equal, 1)
                    }
                }
            };
            return _currentContext.OrganizationService.RetrieveMultiple(query).Entities.FirstOrDefault()?.ToEntity<GeneralPolicy>();
        }
    }
}
