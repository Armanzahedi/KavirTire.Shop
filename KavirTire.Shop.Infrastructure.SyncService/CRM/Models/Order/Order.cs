using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Order
{
    [EntityLogicalName("salesorder")]
    public class Order : EntityWrapper
    {
        public Order() : base(CrmResource.Order)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.Order_Id);
            set
            {
                this.SetAttributeValue(CrmResource.Order_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Order_Id;
            }
        }
        
        public ModelAttribute<EntityReference> Customer
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.Order_Customer);
            set
            {
                this.SetAttributeValue(CrmResource.Order_Customer, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Order_Customer;
            }
        }
        public ModelAttribute<DateTime> RegistrationDate
        {
            get => new ModelAttribute<DateTime>(this, CrmResource.Order_RegistrationDate);
            set
            {
                this.SetAttributeValue(CrmResource.Order_RegistrationDate, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Order_RegistrationDate;
            }
        }
        public ModelAttribute<int> TotalQuantity
        {
            get => new ModelAttribute<int>(this, CrmResource.Order_TotalQuantity);
            set
            {
                this.SetAttributeValue(CrmResource.Order_TotalQuantity, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Order_TotalQuantity;
            }
        }
    }
}