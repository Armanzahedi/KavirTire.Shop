using System;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Entities
{
    public class InvoiceItem : EntityBase
    {
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public Guid ProductId { get; set; }
        public Guid InventoryItemId { get; set; }

        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; } = "/not-found/png";

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
        public decimal TotalPostCost => PostCost * Quantity;
        public decimal PostCost { get; set; }
    }
}

