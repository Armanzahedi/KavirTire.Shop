using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.Enums;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem
{
    [Table("InventoryItems")]
    public class InventoryItem : EntityBase
    {
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        
        public int? Warehouse { get; set; }
        public SyncStatus SyncStatus { get; set; } = SyncStatus.Synced;
        public int? InventoryForSale { get; set; }
        public int? ReservedInventory { get; set; }
        
        [Timestamp]
        public byte[] Version { get; set; }
    }
}