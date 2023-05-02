using EasyCaching.Core;
using KavirTire.Shop.Domain.Products;
using KavirTire.Shop.Infrastructure.Cache;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;

namespace KavirTire.Shop.Infrastructure.Persistence.Repository;

// public class ProductRepository
// {
//     private readonly InventoryItemService _inventoryItemService;
//     private readonly IEasyCachingProvider _cacheProvider;
//     private readonly AppDbContext _dbContext;
//
//     public ProductRepository(InventoryItemService inventoryItemService,
//         IEasyCachingProvider cacheProvider, AppDbContext dbContext)
//     {
//         _inventoryItemService = inventoryItemService;
//         _cacheProvider = cacheProvider;
//         _dbContext = dbContext;
//     }
//
//     public async Task<List<Product>?> GetProducts(CancellationToken cancellationToken = new())
//     {
//         var key = "Products";
//         var products = await _cacheProvider.GetOrCreateAsync(key, () =>
//         {
//             return _dbContext.Products
//                 .Include(x => x.VehicleTypeProducts)
//                 .Include(x => x.PriceListItems)
//                 .Include(x => x.InventoryItems)
//                 .Include(x => x.ProductImages)
//                 .ToListAsync(cancellationToken: cancellationToken);
//         });
//
//         foreach (var product in products)
//         {
//             if (product?.InventoryItems != null)
//                 foreach (var item in product.InventoryItems)
//                 {
//                     
//                 }
//         }
//
//         return products;
//     }
// }