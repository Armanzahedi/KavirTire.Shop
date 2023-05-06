using System;
using System.Data;
using System.Threading.Tasks;
using Hangfire;
using KavirTire.Shop.Infrastructure.SyncService.Common.RecurringJob;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.Enums;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.Specifications;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Enums;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Specifications;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.Services.SynCrmService
{
    [CronSchedule("* * * * *")]
    public class SyncCrmService : IRecurringJob
    {

        private IServiceProvider _serviceProvider;

        public SyncCrmService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [DisableConcurrentExecution(300)]
        public async Task Run()
        {
            using (var scope = _serviceProvider.CreateScope())
            {


                var inventoryItemRepo = scope.ServiceProvider.GetRequiredService<IShopRepository<InventoryItem>>();
                var invoiceRepo = scope.ServiceProvider.GetRequiredService<IShopRepository<Invoice>>();
                var productCrmRepository = scope.ServiceProvider.GetRequiredService<ProductCrmRepository>();
                var inventoryItems = await inventoryItemRepo.ListAsync(new OutOfSyncInventoryItemSpec());
            
                foreach (var inventoryItem in inventoryItems)
                {
                    try
                    {
                        productCrmRepository.UpdateInventoryItem(
                            new Entity(CrmResource.InventoryItem)
                            {
                                Attributes = new AttributeCollection{
                                    {CrmResource.InventoryItem_Id,inventoryItem.Id},
                                    {CrmResource.InventoryItem_InventoryForSale,inventoryItem.InventoryForSale},
                                    {CrmResource.InventoryItem_ReservedInventory,inventoryItem.ReservedInventory}
                                }
                            });
                        inventoryItem.SyncStatus = SyncStatus.Synced;
                        await inventoryItemRepo.UpdateAsync(inventoryItem);
                    }
                    catch (DBConcurrencyException e)
                    {
                        Console.WriteLine(e);
                    }
                }
                
                var invoices = await invoiceRepo.ListAsync(new OutOfSyncInvoiceWithInvoiceItemPaymentSpec());
            
                foreach (var invoice in invoices)
                {
                    if (invoice.InvoiceStatus == InvoiceStatus.Expired)
                    {
                        
                    }
                }
            }

        }
    }
}