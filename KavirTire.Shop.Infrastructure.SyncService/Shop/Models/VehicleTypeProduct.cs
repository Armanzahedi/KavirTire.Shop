using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("VehicleTypeProducts")]
    public class VehicleTypeProduct : EntityBase
    {
        public Guid ProductId { get; set; }
        public Guid VehicleTypeId { get; set; }
        public int ProductType { get; set; }
    }
}