using System.Collections.Generic;
using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class WebPageCrmRepository : CrmRepositoryBase<WebPage>
    {
        public WebPageCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }
        
        public WebPage GetSaleInformationContent()
        {
            var query = new QueryExpression(CrmResource.WebPage)
            {
                ColumnSet = new ColumnSet(CrmResource.WebPage_PartialUrl,CrmResource.WebPage_CopyHtml,CrmResource.WebPage_WebPageLanguage),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.WebFile_PartialUrl, ConditionOperator.Equal, "SaleInformationContent")
                    }
                },
                LinkEntities =
                {
                    new LinkEntity()
                    {
                        LinkFromEntityName = CrmResource.WebPage,
                        LinkToEntityName = CrmResource.WebsiteLanguage,
                        LinkFromAttributeName =  CrmResource.WebPage_WebPageLanguage,
                        LinkToAttributeName = CrmResource.WebsiteLanguage_Id,
                        Columns = new ColumnSet(CrmResource.WebsiteLanguage_PortalLanguage),
                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                LinkFromEntityName = CrmResource.WebsiteLanguage,
                                LinkToEntityName = CrmResource.PortalLanguage,
                                LinkFromAttributeName = CrmResource.WebsiteLanguage_PortalLanguage,
                                LinkToAttributeName =  CrmResource.PortalLanguage_Id,
                                Columns = new ColumnSet(CrmResource.PortalLanguage_LCID),
                                LinkCriteria = new FilterExpression
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression(CrmResource.PortalLanguage_LCID, ConditionOperator.Equal, 1065)
                                    }
                                },
                            }
                        }
                    }
                }
            };

            return serviceProxy.RetrieveMultiple(query).Entities.FirstOrDefault()?.ToEntity<WebPage>();
        }
    }
}