using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("bmsd_locationbase")]
    public class Location : EntityWrapper
    {
        public Location() : base(CrmResource.Location)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.Location_Id);
            set
            {
                this.SetAttributeValue(CrmResource.Location_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Location_Id;
            }
        }
        public ModelAttribute<string> Name
        {
            get => new ModelAttribute<string>(this, CrmResource.Location_Name);
            set
            {
                this.SetAttributeValue(CrmResource.Location_Name, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Location_Name;
            }
        }
        public ModelAttribute<EntityReference> PostCostCategory
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.Location_PostCostCategory);
            set
            {
                this.SetAttributeValue(CrmResource.Location_PostCostCategory, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Location_PostCostCategory;
            }
        }
        public ModelAttribute<EntityReference> ParentLocation
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.Location_ParentLocation);
            set
            {
                this.SetAttributeValue(CrmResource.Location_ParentLocation, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Location_ParentLocation;
            }
        }
    }
}