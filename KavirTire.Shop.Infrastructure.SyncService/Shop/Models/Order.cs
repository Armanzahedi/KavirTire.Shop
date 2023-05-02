using System;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    public class Order : EntityBase
    {
        public Guid? QuoteId { get; set; }
        public Quote Quote { get; set; }
    }
}