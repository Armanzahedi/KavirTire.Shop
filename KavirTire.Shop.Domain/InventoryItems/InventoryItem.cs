using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.InventoryItems.Enums;
using KavirTire.Shop.Domain.Products.Enums;
using KavirTire.Shop.Domain.Products.Exceptions;

namespace KavirTire.Shop.Domain.InventoryItems;

public class InventoryItem : EntityBase<Guid>, IAggregateRoot
{
    public Guid? ProductId { get; set; }

    public Warehouse? Warehouse { get; set; }
    public SyncStatus SyncStatus { get; set; } = SyncStatus.Synced;
    public int? InventoryForSale { get; set; }
    public int? ReservedInventory { get; set; }
    
    [Timestamp]
    public byte[] Version { get; set; }
    public int ReserveProduct(int quantity)
    {
        Guard.Against.Null(quantity, nameof(quantity));
        Guard.Against.Null(InventoryForSale, nameof(InventoryForSale));
        
        if (InventoryForSale < quantity) 
            throw new ProductOutOfStockException();

        InventoryForSale -= quantity;
        ReservedInventory += quantity;

        SyncStatus = SyncStatus.OutOfSync;
        return InventoryForSale!.Value;
    }
    public int ReleaseFromReserve(int quantity)
    {
        Guard.Against.Null(quantity, nameof(quantity));
        Guard.Against.Null(InventoryForSale, nameof(InventoryForSale));

        InventoryForSale += quantity;
        ReservedInventory -= quantity;
        
        SyncStatus = SyncStatus.OutOfSync;
        return InventoryForSale!.Value;
    }
    public int RemoveFromInventory(int quantity)
    {
        Guard.Against.Null(quantity, nameof(quantity));
        Guard.Against.Null(InventoryForSale, nameof(InventoryForSale));

        ReservedInventory -= quantity;
        SyncStatus = SyncStatus.OutOfSync;
        return InventoryForSale!.Value;
    }
}