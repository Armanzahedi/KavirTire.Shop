using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.Invoices.Entities;
using KavirTire.Shop.Domain.Invoices.Enums;
using KavirTire.Shop.Domain.Products;

namespace KavirTire.Shop.Domain.Invoices;

public class Invoice : EntityBase<Guid>, IAggregateRoot
{
    private Invoice()
    {

    }
    public Invoice(string invoiceNumber) {
        //this.InvoiceNumber = invoiceNumber;
    }

    public Invoice(Guid id)
    {
        Id = id;
    }
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? NationalId { get; set; }
    public string? PostalCode { get; set; }
    public string? PostalAddress { get; set; }
    public string? MobilePhone { get; set; }
    public string? Vehicle { get; set; }
    public string? RegistrationPlate { get; set; }
    //public string InvoiceNumber{ get; set; }
    public decimal TotalPostCost { get; set; }
    public decimal TotalInventoryItemCost { get; set; }

    public Guid PriceListId { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; } = InvoiceStatus.Draft;
    public SyncStatus SyncStatus { get; set; } = SyncStatus.OutOfSync;
    public DateTime CreateDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    
    public decimal TotalCost => TotalPostCost + TotalInventoryItemCost + Tax + Charges;
    public decimal Tax => Math.Ceiling(TotalInventoryItemCost * 6/100);
    public decimal Charges => Math.Ceiling(TotalInventoryItemCost * 3/100);
    
    private readonly List<InvoiceItem> _invoiceItems = new();
    public IEnumerable<InvoiceItem>  InvoiceItems =>  _invoiceItems.AsReadOnly();

    public void AddInvoiceItem(Product product, int quantity, Guid priceListId, decimal postCost)
    {
        var inventoryItem = new InvoiceItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductImageUrl = product.MainImage?.ImageUrl ?? "/not-found.png",
            Quantity = quantity,
            InventoryItemId = product.GetInventory()!.Id,
            Amount = product.GetPrice(priceListId),
            PostCost = postCost
        };
     _invoiceItems.Add(inventoryItem);
     TotalPostCost += inventoryItem.TotalPostCost;
     TotalInventoryItemCost += inventoryItem.TotalAmount;
    }

    public void SetExpiration(GeneralPolicy.GeneralPolicy generalPolicy)
    {
        ExpirationDate = DateTime.Now.AddMinutes(generalPolicy.InvoiceExpirationMin);
    }
    public void Close()
    {
        this.InvoiceStatus = InvoiceStatus.Closed;
    }
    public bool IsExpired => ExpirationDate != null && ExpirationDate.Value < DateTime.Now;
}