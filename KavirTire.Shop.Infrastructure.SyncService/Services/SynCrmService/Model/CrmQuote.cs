using System;
using System.Collections.Generic;

namespace KavirTire.Shop.Infrastructure.SyncService.Services.SynCrmService.Model
{
    public class CrmQuote
    {
        public Guid ShopId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Guid PriceListId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int Status { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPostCost { get; set; }
        public decimal TotalInventoryItemCost { get; set; }
        public decimal Tax => Math.Ceiling(TotalInventoryItemCost * 6/100);
        public decimal Charges => Math.Ceiling(TotalInventoryItemCost * 3/100);
        public decimal TotalCost => TotalPostCost + TotalInventoryItemCost + Tax + Charges;

        public List<CrmQuoteProduct> QuoteProducts { get; set; }
        public CrmPayment Payment { get; set; }
        
    }
}