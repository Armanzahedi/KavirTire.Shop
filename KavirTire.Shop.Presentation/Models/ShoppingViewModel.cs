using KavirTire.Shop.Application.Products.Queries.GetProducts;

namespace KavirTire.Shop.Presentation.Models;

public class ShoppingViewModel
{
    public List<ShoppingViewModelProductSection> ProductSections { get; set; } = new();

    public bool HasProduct
    {
        get
        {
            return ProductSections.Any(x => x.Products != null && x.Products.Any());
        }
    }
}

public class ShoppingViewModelProductSection
{
    public ShoppingViewModelProductSection()
    {
        
    }

    public ShoppingViewModelProductSection(int order, string sectionName, List<ProductsQueryResultProduct>? products)
    {
        this.Order = order;
        this.SectionName = sectionName;
        this.Products = products;
    }
    public string SectionName { get; set; }
    public List<ProductsQueryResultProduct>? Products { get; set; }
    public int Order { get; set; }
}