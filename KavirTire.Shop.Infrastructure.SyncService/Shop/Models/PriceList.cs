using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("PriceList")]
    public class PriceList : EntityBase
    {
        public string Name { get; set; } = "";
        public ICollection<PriceListItem> PriceListItems { get; set; }
    }
}