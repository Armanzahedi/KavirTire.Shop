using System;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public EntityReference Product { get; set; }
        public EntityReference WebFile { get; set; }
        public string Data { get; set; }
        public string MimeType { get; set; }
        public string PartialUrl { get; set; }
    }
}