using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("bmsd_vehicletype")]
    public class VehicleType : EntityWrapper
    {
        public VehicleType() : base(CrmResource.VehicleType)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.VehicleType_Id);
            set
            {
                this.SetAttributeValue(CrmResource.VehicleType_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.VehicleType_Id;
            }
        }

        public ModelAttribute<string> Name
        {
            get => new ModelAttribute<string>(this, CrmResource.VehicleType_Name);
            set
            {
                this.SetAttributeValue(CrmResource.VehicleType_Name, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.VehicleType_Name;
            }
        }
    }
}