using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("adx_webpage")]
    public class WebPage : EntityWrapper
    {
        public WebPage() : base(CrmResource.WebPage)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.WebPage_Id);
            set
            {
                this.SetAttributeValue(CrmResource.WebPage_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.WebPage_Id;
            }
        }
        public ModelAttribute<string> PartialUrl
        {
            get => new ModelAttribute<string>(this, CrmResource.WebPage_PartialUrl);
            set
            {
                this.SetAttributeValue(CrmResource.WebPage_PartialUrl, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.WebPage_PartialUrl;
            }
        }
        public ModelAttribute<string> CopyHtml
        {
            get => new ModelAttribute<string>(this, CrmResource.WebPage_CopyHtml);
            set
            {
                this.SetAttributeValue(CrmResource.WebPage_CopyHtml, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.WebPage_CopyHtml;
            }
        }
    }
}