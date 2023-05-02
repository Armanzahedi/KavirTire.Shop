namespace KavirTire.Shop.Application.Cart.Queries.GetCartInfo;

public record GetCartInfoQueryResult
{
    public int MaximumNumberOfPurchases { get; set; }
    public int NumberOfPurchaseItems { get; set; }
    public bool ApplyMaximumNumberOfPurchases { get; set; }
    public bool ApplyPurchaseInterval { get; set; }
    public bool ApplyNumberOfPurchaseItems { get; set; }
    public int CustomerPreviousPurchaseCountInPurchaseInterval { get; set; }
    
    public decimal TirePostCost { get; set; }
}
