using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.IPGs.Entities;
using KavirTire.Shop.Domain.IPGs.Enums;
using Range = KavirTire.Shop.Domain.Common.ValueObjects.Range;

namespace KavirTire.Shop.Domain.IPGs;

public class Ipg : EntityBase<Guid>, IAggregateRoot
{
    public string? Name { get; set; }
    public string? Password { get; set; }
    public Guid? PostBankAccountId { get; set; }
    public string? AcceptorId { get; set; }
    public Bank? Bank { get; set; }
    public string? PassPhrase { get; set; }
    public string? RsaKeyValue { get; set; }
    public string? TerminalId { get; set; }
    public string? ReturnUrl { get; set; }
    public int? SequenceNumber { get; set; }
    public int? DisableFromHour { get; set; }
    public int? DisableFromMinute { get; set; }
    public int? DisableToHour { get; set; }
    public int? DisableToMinute { get; set; }

    public Range.DateRange DisableRange
    {
        get
        { 
            var today = DateTime.Now.Date;

            return new Range.DateRange(today.AddHours(DisableFromHour ?? 0).AddMinutes(DisableFromMinute ?? 0),
                today.AddHours(DisableToHour ?? 0).AddMinutes(DisableToMinute ?? 0));
        }
    }

    private readonly List<BankAccount> _bankAccounts = new();
    public IEnumerable<BankAccount> BankAccounts => _bankAccounts.OrderBy(x=>x.SequenceNumber).ToList().AsReadOnly();
    
    [NotMapped]
    public IReadOnlyList<BankAccount> GatewayBankAccounts => BankAccounts.Where(x=>x.IsPost == false).ToList().AsReadOnly();
}