using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("PostCostCategories")]
    public class PostCostCategory : EntityBase
    {
        public decimal? TirePostCost { get; set; }
    }
}