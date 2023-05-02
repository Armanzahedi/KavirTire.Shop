namespace KavirTire.Shop.Presentation.Models;

public class CartViewModel
{
    public int MaximumNumberOfPurchases { get; set; }
    public int NumberOfPurchaseItems { get; set; }
    public bool ApplyMaximumNumberOfPurchases { get; set; }
    public bool ApplyPurchaseInterval { get; set; }
    public bool ApplyNumberOfPurchaseItems { get; set; }
    public int CustomerPreviousPurchaseCountInPurchaseInterval { get; set; }
}
    
    
