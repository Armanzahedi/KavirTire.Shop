using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;

namespace KavirTire.Shop.Domain.PostCostCategories;

public class PostCostCategory : EntityBase<Guid>, IAggregateRoot
{
    public decimal? TirePostCost { get; set; }
}