using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("GeneralPolicy")]
    public class GeneralPolicy : EntityBase
    {
        public int MaximumNumberOfPurchases { get; set; }
        public int PurchaseInterval { get; set; }
        public int NumberOfPurchaseItems { get; set; }
        public int InvoiceExpirationMin { get; set; }
        public int BasketExpirationInMin { get; set; }
        public bool ShowProductsOnlyRelatedToCustomerCar { get; set; }
        public bool ApplyMaximumNumberOfPurchases { get; set; }
        public bool ApplyPurchaseInterval { get; set; }
        public bool ApplyNumberOfPurchaseItems { get; set; }

        public Guid? PriceListId { get; set; }
        public PriceList PriceList { get; set; }
    }
}