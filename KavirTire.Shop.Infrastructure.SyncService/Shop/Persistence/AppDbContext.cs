using System.Data.Entity;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Entities;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Payment.Entities;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext")
        {
        }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceListItem> PriceListItems { get; set; }
        public DbSet<GeneralPolicy> GeneralPolicy { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WebFile> WebFiles { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<VehicleTypeProduct> VehicleTypeProducts { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<PostCostCategory> PostCostCategories { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<WebPage> WebPages { get; set; }
        public DbSet<Ipg> Ipg { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentLog> PaymentLogs { get; set; }
    }
}