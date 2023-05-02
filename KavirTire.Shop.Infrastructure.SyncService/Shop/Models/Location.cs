using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("Locations")]
    public class Location : EntityBase
    {
        public string Name { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Location Parent { get; set; }
    
        public ICollection<Location> Children;
        public Guid? PostCostCategoryId { get; set; }
    }
}