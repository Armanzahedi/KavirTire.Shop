using System;

namespace KavirTire.Shop.Infrastructure.SyncService.Services.SynCrmService.Model
{
    public class CrmQuoteProduct
    {
        public Guid ShopId { get; set; }
        public Guid ProductId { get; set; }
        public Guid InventoryItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
        public decimal TotalPostCost => PostCost * Quantity;
        public decimal PostCost { get; set; }
    }
}