using System;
using System.Collections.Generic;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    public class Quote : EntityBase
    {
        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }
    
        private readonly List<Product> _products = new List<Product>();
        public IEnumerable<Product> Products => _products.AsReadOnly();
    }
}