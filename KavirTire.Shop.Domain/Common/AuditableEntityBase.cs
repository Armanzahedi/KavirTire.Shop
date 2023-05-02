using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.Common.Attributes;

namespace KavirTire.Shop.Domain.Common;

[Auditable]
public class AuditableEntityBase<TId>: EntityBase<TId>
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}   
public class AuditableEntityBase: AuditableEntityBase<int>
{
}   