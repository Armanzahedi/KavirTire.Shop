using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.PostCostCategories;

namespace KavirTire.Shop.Domain.Locations;

public class Location : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; set; }
    
    public Guid? ParentId { get; set; }
    public virtual Location Parent { get; set; }
    
    private readonly List<Location> _children = new();
    public IEnumerable<Location> Children => _children.AsReadOnly();

    public Guid? PostCostCategoryId { get; set; }
    public PostCostCategory? PostCostCategory { get; set; }

    public decimal? GetPostCost()
    {
        return this.PostCostCategory?.TirePostCost;
    }
}