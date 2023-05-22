using System;
using System.Collections.Generic;
using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Contact;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class ContactCrmRepository : CrmRepositoryBase<Contact>
    {
        public ContactCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }
        public List<Contact> GetActiveContacts(long? lastRowVersion = null)
        {
            
            var query = new QueryExpression(CrmResource.Contact)
            {
                ColumnSet = new ColumnSet(CrmResource.Contact_FirstName,
                    CrmResource.Contact_LastName,
                    CrmResource.Contact_MobilePhone,
                    CrmResource.Contact_NationalId,
                    CrmResource.Contact_PostalAddress,
                    CrmResource.Contact_PostalCode,
                    CrmResource.Contact_Province,
                    CrmResource.Contact_ConfirmPurchaseHistory,
                    CrmResource.Contact_VersionNumber,
                    CrmResource.Contact_AdxUsername
                ),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal,(int)StatusReason.Active)
                    }
                }
            };
            
            if (lastRowVersion != null)
                query.Criteria.Conditions.Add(new ConditionExpression(CrmResource.Contact_VersionNumber, ConditionOperator.GreaterThan, lastRowVersion));
            
            var contacts = GetMoreThan5000WithQueryExpression(query)?.Select(e => e.ToEntity<Contact>()).ToList();

            var personalDocQuery = new QueryExpression(CrmResource.PersonalDoc)
            {
                ColumnSet = new ColumnSet(CrmResource.PersonalDoc_Contact),
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal,
                            (int)PersonalDocStatusReason.Success)
                    }
                }
            };
            var personalDocs = GetMoreThan5000WithQueryExpression(personalDocQuery);

            contacts?.ForEach(c =>
            {
                c.IsApprovedForPurchase = c
                        .ConfirmPurchaseHistory == true && personalDocs
                        .Count(p => p .GetAttributeValueOrDefault<EntityReference>(CrmResource.PersonalDoc_Contact)?.Id ==  c.Id) == 4; // There are 4 types of personal doc that are needed to be approved
            });

            return contacts;
        }
    }
}