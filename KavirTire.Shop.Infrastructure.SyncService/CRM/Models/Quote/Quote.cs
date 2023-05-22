using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Quote
{
    [EntityLogicalName("quote")]
    public class Quote : EntityWrapper
    {
        public Quote() : base(CrmResource.Quote)
        {
        }
    
        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.Quote_Id);
            set
            {
                this.SetAttributeValue(CrmResource.Quote_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Quote_Id;
            }
        }
    
    }
}