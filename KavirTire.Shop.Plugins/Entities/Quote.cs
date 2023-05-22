using KavirTire.Shop.KavirTire.Shop.Plugins.Core.Resources;
using KavirTire.Shop.Plugins.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;

namespace KavirTire.Shop.KavirTire.Shop.Plugins.Entities
{
    [EntityLogicalName("quote")]
    public class Quote : EntityWrapper
    {
        public Quote() : base(PluginResource.Quote)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, PluginResource.Quote_Id);
            set
            {
                this.SetAttributeValue(PluginResource.Quote_Id, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.Quote_Id;
            }
        }
        public ModelAttribute<string> ShopId
        {
            get => new ModelAttribute<string>(this, PluginResource.Quote_ShopId);
            set
            {
                this.SetAttributeValue(PluginResource.Quote_ShopId, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.Quote_ShopId;
                
            }
        }
        public ModelAttribute<OptionSetValue> StatusReason
        {
            get => new ModelAttribute<OptionSetValue>(this, PluginResource.StatusReason);
            set
            {
                this.SetAttributeValue(PluginResource.StatusReason, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.StatusReason;

            }
        }
        public ModelAttribute<OptionSetValue> Status
        {
            get => new ModelAttribute<OptionSetValue>(this, PluginResource.Status);
            set
            {
                this.SetAttributeValue(PluginResource.Status, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.Status;

            }
        }
        public ModelAttribute<string> QuoteNumber
        {
            get => new ModelAttribute<string>(this, PluginResource.Quote_QuoteNumber);
            set
            {
                this.SetAttributeValue(PluginResource.Quote_QuoteNumber, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.Quote_QuoteNumber;

            }
        }
        public ModelAttribute<DateTime> RegistrationDate
        {
            get => new ModelAttribute<DateTime>(this, PluginResource.Quote_RegistrationDate);
            set
            {
                this.SetAttributeValue(PluginResource.Quote_RegistrationDate, value.Value);
                value.Entity = this;
                value.AttributeName = PluginResource.Quote_RegistrationDate;

            }
        }

        public string Name
        {
            get { return this.GetAttributeValue<string>(PluginResource.Name); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Name, value);
            }
        }
        public EntityReference PotentialCustomer
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.Quote_PotentialCustomer); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Quote_PotentialCustomer, value);
            }
        }
        public EntityReference PriceList
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.Quote_PriceList); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Quote_PriceList, value);
            }
        }
        public Money FreightAmount
        {
            get { return this.GetAttributeValue<Money>(PluginResource.Quote_FreightAmount); }

            set
            {
                if (value == null) return;
                this.SetAttributeValue(PluginResource.Quote_FreightAmount, value);
            }
        }
        public decimal TotalTax
        {
            get { return this.GetAttributeValue<Money>(PluginResource.Quote_TotalTax).Value; }

            set
            {
                this.SetAttributeValue(PluginResource.Quote_TotalTax, value);
            }
        }
        public decimal TotalPreFreightAmount
        {
            get { return this.GetAttributeValue<Money>(PluginResource.Quote_TotalPreFreightAmount).Value; }

            set
            {
                this.SetAttributeValue(PluginResource.Quote_TotalPreFreightAmount, value);
            }
        }
        public decimal TotalAmountWithoutTax
        {
            get { return this.GetAttributeValue<Money>(PluginResource.Quote_TotalAmountWithoutTax).Value; }

            set
            {
                this.SetAttributeValue(PluginResource.Quote_TotalAmountWithoutTax, value);
            }
        }
        public decimal TotalAmount
        {
            get { return this.GetAttributeValue<Money>(PluginResource.Quote_TotalAmount).Value; }

            set
            {
                this.SetAttributeValue(PluginResource.Quote_TotalAmount, value);
            }
        }
        public DateTime? ExpiredDate
        {
            get { return this.GetAttributeValue<DateTime?>(PluginResource.Quote_ExpiredDate); }

            set
            {
                this.SetAttributeValue(PluginResource.Quote_ExpiredDate, value);
            }
        }
        public EntityReference Currency
        {
            get { return this.GetAttributeValue<EntityReference>(PluginResource.Quote_Currency); }

            set
            {
                this.SetAttributeValue(PluginResource.Quote_Currency, value);
            }
        }
    }
}
