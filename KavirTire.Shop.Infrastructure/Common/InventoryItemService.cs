using EasyCaching.Core;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Infrastructure.Cache;

namespace KavirTire.Shop.Infrastructure.Common;

// public class InventoryItemService : IInventoryItemService
// {
//     private readonly IEasyCachingProvider _cacheProvider;
//     private readonly IReadRepository<InventoryItem> _inventoryItemRepository;
//     
//     public InventoryItemService(IEasyCachingProvider cachingProvider, IReadRepository<InventoryItem> inventoryItemRepository)
//     {
//         _cacheProvider = cachingProvider;
//         _inventoryItemRepository = inventoryItemRepository;
//     }
//
//     private static string Prefix => "Inv";
//
//     public async Task<InventoryItem?> GetByIdAsync(Guid inventoryItemId,CancellationToken cancellationToken = new ())
//     {
//         var key = $"{Prefix}-{inventoryItemId.ToString()}";
//         return await _cacheProvider.GetOrCreateAsync(key, () => _inventoryItemRepository.GetByIdAsync(inventoryItemId, cancellationToken));
//     }
//     public async Task UpdateAsync(InventoryItem inventoryItem,CancellationToken cancellationToken = new ())
//     {
//         var key = $"{Prefix}-{inventoryItem.Id.ToString()}";
//         await _cacheProvider.SetAsync<InventoryItem>(key, inventoryItem, TimeSpan.FromDays(1), cancellationToken);
//     }
//     public async Task PrepareInventoryItems(CancellationToken cancellationToken = new ())
//     {
//         var inventoryItems = await _inventoryItemRepository.ListAsync(cancellationToken);
//         foreach (var item in inventoryItems)
//         {
//             await UpdateAsync(item, cancellationToken);
//         }
//     }
// }

