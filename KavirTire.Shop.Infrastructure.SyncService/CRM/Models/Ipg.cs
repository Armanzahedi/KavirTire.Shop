using System;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models
{
    public class Ipg
    {
        public Guid IpgId { get; set; }
        public string Name { get; set; }
        public string TerminalId { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public int SequenceNumber { get; set; }
        public EntityReference PostBankAccount { get; set; }
        public int? BankType { get; set; }
        public string AcceptorId { get; set; }
        public string RsaKeyValue { get; set; }
        public string PassPhrase { get; set; }        
        public int? StartStopHour { get; set; }        
        public int? StartStopMinute { get; set; }        
        public int? FinishStopHour { get; set; }        
        public int? FinishStopMinute { get; set; }        
        
        
    }
}