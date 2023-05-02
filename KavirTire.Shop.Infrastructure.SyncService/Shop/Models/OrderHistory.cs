using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("OrderHistory")]
    public class OrderHistory : EntityBase
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}