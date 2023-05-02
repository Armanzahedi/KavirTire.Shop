using System;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public EntityReference Product { get; set; }
        public int? Warehouse { get; set; }
        public int? InventoryForSale { get; set; }
        public int? ReservedInventory { get; set; }
    }
}