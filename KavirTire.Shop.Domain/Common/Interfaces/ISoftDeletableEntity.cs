namespace KavirTire.Shop.Domain.Common.Interfaces;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}