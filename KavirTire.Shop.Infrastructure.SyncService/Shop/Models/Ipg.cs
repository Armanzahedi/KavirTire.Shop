using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("Ipg")]
    public class Ipg : EntityBase
    {
        public string ReturnUrl { get; set; }
        
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid? PostBankAccountId { get; set; }
        public string AcceptorId { get; set; }
        public int? Bank { get; set; }
        public string PassPhrase { get; set; }
        public string RsaKeyValue { get; set; }
        public string TerminalId { get; set; }
        public int? SequenceNumber { get; set; }
        public int? DisableFromHour { get; set; }
        public int? DisableFromMinute { get; set; }
        public int? DisableToHour { get; set; }
        public int? DisableToMinute { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}