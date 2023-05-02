using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("VehicleTypes")]
    public class VehicleType : EntityBase
    {
        public string Name { get; set; }
        
        public ICollection<VehicleTypeProduct> VehicleTypeProducts;
        public ICollection<Vehicle> Vehicles;
    }
}