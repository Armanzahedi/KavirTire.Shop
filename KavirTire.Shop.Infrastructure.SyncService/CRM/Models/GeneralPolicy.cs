using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    [EntityLogicalName("bmsd_generalpolicy")]
    public class GeneralPolicy : EntityWrapper
    {
        public GeneralPolicy() : base(CrmResource.GeneralPolicy)
        {
        }
        
        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.GeneralPolicy_Id);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_Id;
            }
        }
             
        public ModelAttribute<int> MaximumNumberOfPurchases
        {
            get => new ModelAttribute<int>(this, CrmResource.GeneralPolicy_MaximumNumberOfPurchases);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_MaximumNumberOfPurchases, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_MaximumNumberOfPurchases;
            }
        }    
        public ModelAttribute<int> PurchaseIntervalInDays
        {
            get => new ModelAttribute<int>(this, CrmResource.GeneralPolicy_PurchaseIntervalInDays);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_PurchaseIntervalInDays, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_PurchaseIntervalInDays;
            }
        }   
        public ModelAttribute<int> NumberOfPurchaseItems
        {
            get => new ModelAttribute<int>(this, CrmResource.GeneralPolicy_NumberOfPurchaseItems);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_NumberOfPurchaseItems, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_NumberOfPurchaseItems;
            }
        }
        public ModelAttribute<bool> ShowProductsOnlyRelatedToCustomerCar
        {
            get => new ModelAttribute<bool>(this, CrmResource.GeneralPolicy_ShowProductsOnlyRelatedToCustomerCar);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_ShowProductsOnlyRelatedToCustomerCar, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_ShowProductsOnlyRelatedToCustomerCar;
            }
        }
        
        public ModelAttribute<int> ExpireQuoteForCookieMin
        {
            get => new ModelAttribute<int>(this, CrmResource.GeneralPolicy_ExpireQuoteForCookieMin);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_ExpireQuoteForCookieMin, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_ExpireQuoteForCookieMin;
            }
        }
        public ModelAttribute<int> ExpireQuoteForActionMin
        {
            get => new ModelAttribute<int>(this, CrmResource.GeneralPolicy_ExpireQuoteForActionMin);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_ExpireQuoteForActionMin, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_ExpireQuoteForActionMin;
            }
        }
        public ModelAttribute<bool> ApplyMaximumNumberOfPurchases
        {
            get => new ModelAttribute<bool>(this, CrmResource.GeneralPolicy_ApplyMaximumNumberOfPurchases);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_ApplyMaximumNumberOfPurchases, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_ApplyMaximumNumberOfPurchases;
            }
        }
        public ModelAttribute<bool> ApplyPurchaseIntervalInDays
        {
            get => new ModelAttribute<bool>(this, CrmResource.GeneralPolicy_ApplyPurchaseIntervalInDays);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_ApplyPurchaseIntervalInDays, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_ApplyPurchaseIntervalInDays;
            }
        }
        public ModelAttribute<bool> ApplyNumberOfPurchaseItems
        {
            get => new ModelAttribute<bool>(this, CrmResource.GeneralPolicy_ApplyNumberOfPurchaseItems);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_ApplyNumberOfPurchaseItems, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_ApplyNumberOfPurchaseItems;
            }
        }
        public ModelAttribute<EntityReference> PriceList
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.GeneralPolicy_PriceList);
            set
            {
                this.SetAttributeValue(CrmResource.GeneralPolicy_PriceList, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.GeneralPolicy_PriceList;
            }
        }
    }
}