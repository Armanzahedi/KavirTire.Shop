using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("bmsd_vehicle")]
    public class Vehicle : EntityWrapper
    {
        public Vehicle() : base(CrmResource.Vehicle)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.Vehicle_Id);
            set
            {
                this.SetAttributeValue(CrmResource.Vehicle_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Vehicle_Id;
            }
        }
        public ModelAttribute<EntityReference> VehicleType
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.Vehicle_VehicleType);
            set
            {
                this.SetAttributeValue(CrmResource.Vehicle_VehicleType, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Vehicle_VehicleType;
            }
        }
        public ModelAttribute<EntityReference> Customer
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.Vehicle_Customer);
            set
            {
                this.SetAttributeValue(CrmResource.Vehicle_Customer, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Vehicle_Customer;
            }
        }
        public ModelAttribute<int?> VehicleRegistrationNumberLeft
        {
            get => new ModelAttribute<int?>(this, CrmResource.Vehicle_VehicleRegistrationNumberLeft);
            set
            {
                this.SetAttributeValue(CrmResource.Vehicle_VehicleRegistrationNumberLeft, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Vehicle_VehicleRegistrationNumberLeft;
            }
        }
        public ModelAttribute<int?> VehicleRegistrationNumberMiddle
        {
            get => new ModelAttribute<int?>(this, CrmResource.Vehicle_VehicleRegistrationNumberMiddle);
            set
            {
                this.SetAttributeValue(CrmResource.Vehicle_VehicleRegistrationNumberMiddle, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Vehicle_VehicleRegistrationNumberMiddle;
            }
        }
        public ModelAttribute<int?> VehicleRegistrationNumberRight
        {
            get => new ModelAttribute<int?>(this, CrmResource.Vehicle_VehicleRegistrationNumberRight);
            set
            {
                this.SetAttributeValue(CrmResource.Vehicle_VehicleRegistrationNumberRight, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Vehicle_VehicleRegistrationNumberRight;
            }
        }
        public ModelAttribute<OptionSetValue> VehicleRegistrationCharacter
        {
            get => new ModelAttribute<OptionSetValue>(this, CrmResource.Vehicle_VehicleRegistrationCharacter);
            set
            {
                this.SetAttributeValue(CrmResource.Vehicle_VehicleRegistrationCharacter, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Vehicle_VehicleRegistrationCharacter;
            }
        }
    }
}