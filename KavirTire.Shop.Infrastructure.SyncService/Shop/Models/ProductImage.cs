using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("ProductImages")]
    public class ProductImage : EntityBase
    {
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }

    }
}