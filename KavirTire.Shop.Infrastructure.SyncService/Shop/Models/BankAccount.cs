using System;
using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("BankAccounts")]
    public class BankAccount : EntityBase
    {
        public Guid? IpgId { get; set; }
        public Ipg Ipg { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public int? SequenceNumber { get; set; }
        public bool? IsPost { get; set; }
        public string Iban { get; set; }
        public string ImageUrl { get; set; }
    }
}