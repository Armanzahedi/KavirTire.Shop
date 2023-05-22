using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using KavirTire.Shop.Infrastructure.SyncService.Common.RecurringJob;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Repository;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Resources;
using KavirTire.Shop.Infrastructure.SyncService.Services.SynCrmService.Model;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.Enums;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.Specifications;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Enums;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Specifications;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment.Enums;
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
                var quoteRepository = scope.ServiceProvider.GetRequiredService<QuoteCrmRepository>();
                
                foreach (var invoice in invoices)
                {
                    var quote = CreateQuote(invoice);
                    try
                    {
                        quoteRepository.SyncQuote(quote);
                        invoice.SyncStatus = InvoiceSyncStatus.Synced;
                        await invoiceRepo.UpdateAsync(invoice);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

        }

        private static CrmQuote CreateQuote(Invoice invoice)
        {
            var quote = new CrmQuote
            {
                ShopId = invoice.Id,
                RegistrationDate = invoice.CreateDate,
                PriceListId = invoice.PriceListId,
                ExpirationDate = invoice.ExpirationDate,
                Status = (int)invoice.InvoiceStatus,
                CustomerId = invoice.CustomerId,
                CustomerName = invoice.CustomerName,
                TotalPostCost = invoice.TotalPostCost,
                TotalInventoryItemCost = invoice.TotalInventoryItemCost,
                QuoteProducts = invoice?.InvoiceItems?.Select(invoiceItem => new CrmQuoteProduct
                {
                    ShopId = invoiceItem.Id,
                    ProductId = invoiceItem.ProductId,
                    InventoryItemId = invoiceItem.InventoryItemId,
                    Quantity = invoiceItem.Quantity,
                    Price = invoiceItem.Price,
                    PostCost = invoiceItem.PostCost
                }).ToList(),
                Payment = invoice.Payments?.Where(x=>x.Status == PaymentStatus.Confirmed)?.Select(p => new CrmPayment
                {
                    Amount = p.Amount,
                    IpgId = p.IpgId,
                    BankAccountId = p.BankAccountId,
                    PostBankAccountId = p.PostBankAccountId,
                    CreateDate = p.CreateDate,
                    ProcessDate = p.ProcessDate,
                    PaymentIdentity = p.PaymentIdentity,
                    SystemTraceNo = p.SystemTraceNo,
                    RRN = p.RRN,
                    RefNo = p.RefNo,
                    ShopResNo = p.ResNo,
                    SecurePan = p.SecurePan,
                    PaymentLogs = p.PaymentLogs.Select(log => new CrmPaymentLog
                    {
                        PaymentId = log.Id,
                        Type = (int)log.Type,
                        CreateDate = log.CreateDate,
                        Message = log.Message,
                        ErrorCode = log.ErrorCode,
                        Details = log.Details
                    }).ToList()
                }).FirstOrDefault()
            };
            return quote;
        }
    }
}