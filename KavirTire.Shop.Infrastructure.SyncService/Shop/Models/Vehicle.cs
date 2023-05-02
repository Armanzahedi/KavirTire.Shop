using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("Vehicles")]
    public class Vehicle : EntityBase
    {
        public Guid? VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
    
        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public int? RegistrationPlateNumberLeft { get; set; }
        public int? RegistrationPlateNumberMiddle { get; set; }
        public int? RegistrationPlateNumberRight { get; set; }
        public int? RegistrationPlateCharacter { get; set; }
    }
}