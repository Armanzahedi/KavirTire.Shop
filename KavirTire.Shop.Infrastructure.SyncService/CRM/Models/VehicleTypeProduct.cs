using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("bmsd_kv_vehicletypeproduct")]
    public class VehicleTypeProduct : EntityWrapper
    {
        public VehicleTypeProduct() : base(CrmResource.VehicleTypeProduct)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.VehicleTypeProduct_Id);
            set
            {
                this.SetAttributeValue(CrmResource.VehicleTypeProduct_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.VehicleTypeProduct_Id;
            }
        }
        public ModelAttribute<EntityReference> VehicleType
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.VehicleTypeProduct_VehicleType);
            set
            {
                this.SetAttributeValue(CrmResource.VehicleTypeProduct_VehicleType, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.VehicleTypeProduct_VehicleType;
            }
            
        }
        public ModelAttribute<EntityReference> Product
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.VehicleTypeProduct_Product);
            set
            {
                this.SetAttributeValue(CrmResource.VehicleTypeProduct_Product, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.VehicleTypeProduct_Product;
            }
        }
        public ModelAttribute<OptionSetValue> Type
        {
            get => new ModelAttribute<OptionSetValue>(this, CrmResource.VehicleTypeProduct_Type);
            set
            {
                this.SetAttributeValue(CrmResource.VehicleTypeProduct_Type, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.VehicleTypeProduct_Type;
            }
        }
    }
}