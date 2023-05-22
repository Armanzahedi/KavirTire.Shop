using System;
using System.Collections.Generic;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment.Entities;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment.Enums;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment
{
    public class Payment : EntityBase
    {
        public decimal Amount { get; set; }
        public Guid CustomerId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid IpgId { get; set; }
        public Guid BankAccountId { get; set; }
        public Guid? PostBankAccountId { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ProcessDate { get; set; }
    
        public string PaymentIdentity { get; set; }
        public string SystemTraceNo { get; set; }
        public long? RRN { get; set; }
        public string RefNo { get; set; }
        public string ResNo { get;set; }
        public string SecurePan { get; set; }
        
        public ICollection<PaymentLog> PaymentLogs { get; set; }
    }
}