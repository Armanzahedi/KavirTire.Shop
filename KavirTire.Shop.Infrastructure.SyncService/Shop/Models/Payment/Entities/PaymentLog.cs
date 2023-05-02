using System;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment.Enums;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment.Entities
{
    public class PaymentLog : EntityBase
    {
        public PaymentLog()
        {
            CreateDate = DateTime.Now;
        }
        public Guid PaymentId { get; set; }

        public PaymentLogType Type { get; set; }
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string Details { get; set; }
    
    }  
}

