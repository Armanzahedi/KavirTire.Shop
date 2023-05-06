using System;

namespace KavirTire.Shop.Infrastructure.SyncService.Services.SynCrmService.Model
{
    public class CrmQuote
    {
        public Guid ShopId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Guid PriceListId { get; set; }
        public Guid ExpirationDate { get; set; }
        public int Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid LocationId { get; set; }
        public decimal TotalPostCost { get; set; }
        public decimal TotalInventoryItemCost { get; set; }
        public decimal Tax => Math.Ceiling(TotalInventoryItemCost * 6/100);
        public decimal Charges => Math.Ceiling(TotalInventoryItemCost * 3/100);
        public decimal TotalCost => TotalPostCost + TotalInventoryItemCost + Tax + Charges;

        
    }
}