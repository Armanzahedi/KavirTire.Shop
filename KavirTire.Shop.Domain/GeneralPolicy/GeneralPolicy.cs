using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.PriceLists;

namespace KavirTire.Shop.Domain.GeneralPolicy;

public class GeneralPolicy : EntityBase<Guid>, IAggregateRoot
{
    public int MaximumNumberOfPurchases { get; set; }
    public int PurchaseInterval { get; set; }
    public int NumberOfPurchaseItems { get; set; }
    public int BasketExpirationInMin { get; set; } = 100;
    public int InvoiceExpirationMin { get; set; } = 10;
    public bool ShowProductsOnlyRelatedToCustomerCar { get; set; }
    public bool ApplyMaximumNumberOfPurchases { get; set; }
    public bool ApplyPurchaseInterval { get; set; }
    public bool ApplyNumberOfPurchaseItems { get; set; }
    
    public Guid? PriceListId { get; set; }
    public PriceList? PriceList { get; set; }
}