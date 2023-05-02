using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("Products")]
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public ICollection<VehicleTypeProduct> VehicleTypeProducts;
        public ICollection<InventoryItem.InventoryItem> InventoryItems;
        public ICollection<PriceListItem> PriceListItems;
        public ICollection<ProductImage> ProductImages;

    }
}