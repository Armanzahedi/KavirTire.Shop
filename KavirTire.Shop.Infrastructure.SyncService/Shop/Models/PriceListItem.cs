using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("PriceListItems")]
    public class PriceListItem : EntityBase
    {
        public Guid? ProductId { get; set; }
        public Guid? PriceListId { get; set; }
        public decimal Amount { get; set; }
    }
}