using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("Customers")]
    public class Customer : EntityBase
    {
    
        private readonly List<Vehicle> _vehicles = new List<Vehicle>();
        public IEnumerable<Vehicle> Vehicles => _vehicles.AsReadOnly();

        private readonly List<Quote> _quotes = new List<Quote>();
        public IEnumerable<Quote> Quotes => _quotes.AsReadOnly();
    
        private readonly List<Order> _orders = new List<Order>();
        public IEnumerable<Order> Orders => _orders.AsReadOnly();
    
        public Guid? ProvinceId { get; set; }
        public Location Province { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public string PostalAddress { get; set; }
        public string MobilePhone { get; set; }
        public bool IsApprovedForPurchase { get; set; }
        
        public long? CrmRowVersion { get; set; }
    }
}