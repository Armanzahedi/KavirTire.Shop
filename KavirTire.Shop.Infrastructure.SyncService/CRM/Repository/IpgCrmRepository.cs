using System;
using System.Collections.Generic;
using System.Linq;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Config;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Enums;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Repository
{
    public class IpgCrmRepository : CrmRepositoryBase
    {
        public IpgCrmRepository(ICrmSettingManager currentCrmSettingManager) : base(currentCrmSettingManager)
        {
        }

        public List<Ipg> GetIpgs()
        {
            var query = new QueryExpression(CrmResource.Ipg)
            {
                ColumnSet = new ColumnSet(new[]
                {
                    CrmResource.Ipg_TerminalId,
                    CrmResource.Ipg_Password,
                    CrmResource.Ipg_ReturnUrl,
                    CrmResource.Ipg_SequenceNumber,
                    CrmResource.Ipg_Name,
                    CrmResource.Ipg_PostBankAccount,
                    CrmResource.Ipg_Bank,
                    CrmResource.Ipg_AcceptorId,
                    CrmResource.Ipg_RSAKeyValue,
                    CrmResource.Ipg_PassPhrase,
                    CrmResource.Ipg_StartStopHour,
                    CrmResource.Ipg_StartStopMin,
                    CrmResource.Ipg_FinishStopHour,
                    CrmResource.Ipg_FinishStopMin
                }),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                    }
                }
            };
            var retrieveMultipleResponse =
                (RetrieveMultipleResponse)this.serviceProxy.Execute(new RetrieveMultipleRequest { Query = query });

            return retrieveMultipleResponse?.EntityCollection?.Entities
                ?.Select(x => new Ipg
                {
                    IpgId = x.GetAttributeValueOrDefault<Guid>(CrmResource.Ipg_Id),
                    Name = x.GetAttributeValueOrDefault<string>(CrmResource.Ipg_Name),
                    TerminalId = x.GetAttributeValueOrDefault<string>(CrmResource.Ipg_TerminalId),
                    Password = x.GetAttributeValueOrDefault<string>(CrmResource.Ipg_Password),
                    ReturnUrl = x.GetAttributeValueOrDefault<string>(CrmResource.Ipg_ReturnUrl),
                    SequenceNumber = x.GetAttributeValueOrDefault<int>(CrmResource.Ipg_SequenceNumber),
                    PostBankAccount = x.GetAttributeValueOrDefault<EntityReference>(CrmResource.Ipg_PostBankAccount),
                    BankType = x.GetAttributeValueOrDefault<OptionSetValue>(CrmResource.Ipg_Bank)?.Value,
                    AcceptorId = x.GetAttributeValueOrDefault<string>(CrmResource.Ipg_AcceptorId),
                    RsaKeyValue = x.GetAttributeValueOrDefault<string>(CrmResource.Ipg_RSAKeyValue),
                    PassPhrase = x.GetAttributeValueOrDefault<string>(CrmResource.Ipg_PassPhrase),
                    StartStopHour = x.GetAttributeValueOrDefault<int?>(CrmResource.Ipg_StartStopHour),
                    StartStopMinute = x.GetAttributeValueOrDefault<int?>(CrmResource.Ipg_StartStopMin),
                    FinishStopHour = x.GetAttributeValueOrDefault<int?>(CrmResource.Ipg_FinishStopHour),
                    FinishStopMinute = x.GetAttributeValueOrDefault<int?>(CrmResource.Ipg_FinishStopMin),

                }).ToList();
        }

        public List<BankAccount> GetBankAccounts()
        {
            var query = new QueryExpression(CrmResource.BankAccount)
            {
                ColumnSet = new ColumnSet(new[]
                {
                    CrmResource.BankAccount_Iban,
                    CrmResource.BankAccount_Name,
                    CrmResource.BankAccount_Ipg,
                    CrmResource.BankAccount_BankName,
                    CrmResource.BankAccount_IsPost,
                    CrmResource.BankAccount_SequenceNumber,
                    CrmResource.BankAccount_WebFile
                }),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal, (int)Status.Active),
                    }
                },
                LinkEntities =
                {
                    new LinkEntity()
                    {
                        JoinOperator = JoinOperator.LeftOuter,
                        LinkFromEntityName = CrmResource.BankAccount,
                        LinkToEntityName = CrmResource.WebFile,
                        LinkFromAttributeName = CrmResource.BankAccount_WebFile,
                        LinkToAttributeName = CrmResource.WebFile_Id,
                        Columns = new ColumnSet(CrmResource.WebFile_PartialUrl),
                        LinkCriteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression(CrmResource.Entity_Status, ConditionOperator.Equal,
                                    (int)Status.Active),
                                new ConditionExpression(CrmResource.Entity_StatusReason, ConditionOperator.Equal,
                                    (int)StatusReason.Active),
                            }
                        },
                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                JoinOperator = JoinOperator.LeftOuter,
                                Columns = new ColumnSet(
                                    CrmResource.Note_Id,
                                    CrmResource.Note_DocumentBody,
                                    CrmResource.Note_DocumentMimeType),
                                LinkFromEntityName = CrmResource.WebFile,
                                LinkToEntityName = CrmResource.Note,
                                LinkFromAttributeName = CrmResource.WebFile_Id,
                                LinkToAttributeName = CrmResource.Note_Object,
                            }
                        }
                    }
                }
            };
            var retrieveMultipleResponse =
                (RetrieveMultipleResponse)this.serviceProxy.Execute(new RetrieveMultipleRequest { Query = query });

            return retrieveMultipleResponse?.EntityCollection?.Entities
                ?.Select(x => new BankAccount
                {
                    Id = x.GetAttributeValueOrDefault<Guid>(CrmResource.BankAccount_Id),
                    Ipg = x.GetAttributeValueOrDefault<EntityReference>(CrmResource.BankAccount_Ipg),
                    Name = x.GetAttributeValueOrDefault<string>(CrmResource.BankAccount_Name),
                    BankName = x.GetAttributeValueOrDefault<string>(CrmResource.BankAccount_BankName),
                    SequenceNumber = x.GetAttributeValueOrDefault<int>(CrmResource.BankAccount_SequenceNumber),
                    IsPost = x.GetAttributeValueOrDefault<bool>(CrmResource.BankAccount_IsPost),
                    Iban = x.GetAttributeValueOrDefault<string>(CrmResource.BankAccount_Iban),
                    MimeType = (string)x.GetAttributeValueOrDefault<AliasedValue>("annotation2.mimetype")?.Value,
                    Data = (string)x.GetAttributeValueOrDefault<AliasedValue>("annotation2.documentbody")?.Value,
                    ImageUrl = (string)x.GetAttributeValueOrDefault<AliasedValue>("adx_webfile1.adx_partialurl")?.Value,
                    WebFile = x.GetAttributeValueOrDefault<EntityReference>(CrmResource.BankAccount_WebFile)
                }).ToList();
        }
    }
}