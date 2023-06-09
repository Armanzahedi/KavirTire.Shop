﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KavirTire.Shop.Plugins.Models
{
    public class CrmPayment
    {
        public decimal Amount { get; set; }
        public Guid IpgId { get; set; }
        public Guid BankAccountId { get; set; }
        public Guid? PostBankAccountId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? ProcessDate { get; set; }

        public string PaymentIdentity { get; set; }
        public string SystemTraceNo { get; set; }
        public long? RRN { get; set; }
        public string RefNo { get; set; }
        public string ShopResNo { get; set; }
        public string SecurePan { get; set; }
        public List<CrmPaymentLog> PaymentLogs { get; set; }

    }
    public class CrmPaymentLog
    {
        public Guid PaymentId { get; set; }
        public int Type { get; set; }
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string Details { get; set; }

    }
}
