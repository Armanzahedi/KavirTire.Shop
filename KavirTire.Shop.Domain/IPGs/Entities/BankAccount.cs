using KavirTire.Shop.Domain.Common;

namespace KavirTire.Shop.Domain.IPGs.Entities;

public class BankAccount : EntityBase<Guid>
{
    public Guid? IpgId { get; set; }

    public string? Name { get; set; }
    public string? BankName { get; set; }
    public int? SequenceNumber { get; set; }
    public bool? IsPost { get; set; }
    public string? Iban { get; set; }
    public string? ImageUrl { get; set; }
}