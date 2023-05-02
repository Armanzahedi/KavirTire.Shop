using System;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    public class BankAccount
    {

        public Guid Id { get; set; }
        public EntityReference Ipg { get; set; }

        public string Name { get; set; }
        public string BankName { get; set; }
        public int? SequenceNumber { get; set; }
        public bool? IsPost { get; set; }
        public string Iban { get; set; }
        public string ImageUrl { get; set; }
        public string Data { get; set; }
        public string MimeType { get; set; }
        public EntityReference WebFile { get; set; }
    }
}