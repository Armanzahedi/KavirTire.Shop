using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Contact
{
    [EntityLogicalName("contact")]
    public class Contact : EntityWrapper
    {
        public Contact() : base(CrmResource.Contact)
        {
        }

        public ModelAttribute<Guid> Id
        {
            get => new ModelAttribute<Guid>(this, CrmResource.Contact_Id);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_Id, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_Id;
            }
        }

        public ModelAttribute<string> FirstName
        {
            get => new ModelAttribute<string>(this, CrmResource.Contact_FirstName);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_FirstName, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_FirstName;
            }
        }

        public ModelAttribute<string> LastName
        {
            get => new ModelAttribute<string>(this, CrmResource.Contact_LastName);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_LastName, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_LastName;
            }
        }

        public ModelAttribute<string> MobilePhone
        {
            get => new ModelAttribute<string>(this, CrmResource.Contact_MobilePhone);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_MobilePhone, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_MobilePhone;
            }
        }
        public ModelAttribute<string> NationalId
        {
            get => new ModelAttribute<string>(this, CrmResource.Contact_NationalId);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_NationalId, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_NationalId;
            }
        }
        
        public ModelAttribute<string> PostalCode
        {
            get => new ModelAttribute<string>(this, CrmResource.Contact_PostalCode);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_PostalCode, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_PostalCode;
            }
        }
        public ModelAttribute<string> PostalAddress
        {
            get => new ModelAttribute<string>(this, CrmResource.Contact_PostalAddress);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_PostalAddress, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_PostalAddress;
            }
        }
        public ModelAttribute<EntityReference> Province
        {
            get => new ModelAttribute<EntityReference>(this, CrmResource.Contact_Province);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_Province, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_Province;
            }
        }      
        public ModelAttribute<bool> ConfirmPurchaseHistory
        {
            get => new ModelAttribute<bool>(this, CrmResource.Contact_ConfirmPurchaseHistory);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_ConfirmPurchaseHistory, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_ConfirmPurchaseHistory;
            }
        }
        public ModelAttribute<long> VersionNumber
        {
            get => new ModelAttribute<long>(this, CrmResource.Contact_VersionNumber);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_VersionNumber, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_VersionNumber;
            }
        }
        public ModelAttribute<string> AdxUsername
        {
            get => new ModelAttribute<string>(this, CrmResource.Contact_AdxUsername);
            set
            {
                this.SetAttributeValue(CrmResource.Contact_AdxUsername, value.Value);
                value.Entity = this;
                value.AttributeName = CrmResource.Contact_AdxUsername;
            }
        }
        public bool IsApprovedForPurchase { get; set; }
    }
}