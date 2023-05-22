using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KavirTire.Shop.Plugins.Models
{
    public enum CrmQuoteStatus
    {
        Expired = 1,
        Closed = 2
    }
    public class CrmQuote
    {
        public Guid ShopId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Guid PriceListId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public CrmQuoteStatus Status { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public decimal TotalPostCost { get; set; }
        public decimal TotalInventoryItemCost { get; set; }
        public decimal Tax => Math.Ceiling(TotalInventoryItemCost * 6 / 100);
        public decimal Charges => Math.Ceiling(TotalInventoryItemCost * 3 / 100);
        public decimal TotalCost => TotalPostCost + TotalInventoryItemCost + Tax + Charges;

        public List<CrmQuoteProduct> QuoteProducts { get; set; }
        public CrmPayment Payment { get; set; }

    }
}
