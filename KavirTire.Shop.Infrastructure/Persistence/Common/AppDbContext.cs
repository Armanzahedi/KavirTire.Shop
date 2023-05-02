using System.Reflection;
using KavirTire.Shop.Domain.Customers;
using KavirTire.Shop.Domain.GeneralPolicy;
using KavirTire.Shop.Domain.InventoryItems;
using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Domain.Invoices.Entities;
using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Domain.IPGs.Entities;
using KavirTire.Shop.Domain.Locations;
using KavirTire.Shop.Domain.OrderHistory;
using KavirTire.Shop.Domain.Payments;
using KavirTire.Shop.Domain.Payments.Entities;
using KavirTire.Shop.Domain.PostCostCategories;
using KavirTire.Shop.Domain.PriceLists;
using KavirTire.Shop.Domain.PriceLists.Entities;
using KavirTire.Shop.Domain.Products;
using KavirTire.Shop.Domain.Products.Entities;
using KavirTire.Shop.Domain.Vehicles;
using KavirTire.Shop.Domain.VehicleTypes;
using KavirTire.Shop.Domain.VehicleTypes.Entities;
using KavirTire.Shop.Domain.WebFiles;
using KavirTire.Shop.Domain.WebPages;
using KavirTire.Shop.Infrastructure.Common;
using KavirTire.Shop.Infrastructure.Persistence.Audit;
using KavirTire.Shop.Infrastructure.Persistence.Audit.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KavirTire.Shop.Infrastructure.Persistence.Common;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly AuditSaveChangesInterceptor _auditSaveChangesInterceptor;
    private readonly SoftDeleteSaveChangeInterceptor _softDeleteSaveChangeInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IMediator mediator,
        AuditSaveChangesInterceptor auditSaveChangesInterceptor,
        SoftDeleteSaveChangeInterceptor softDeleteSaveChangeInterceptor) 
        : base(options)
    {
        _mediator = mediator;
        _auditSaveChangesInterceptor = auditSaveChangesInterceptor;
        _softDeleteSaveChangeInterceptor = softDeleteSaveChangeInterceptor;
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<GeneralPolicy> GeneralPolicy => Set<GeneralPolicy>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<PostCostCategory> PostCostCategories => Set<PostCostCategory>();
    public DbSet<PriceList> PriceList => Set<PriceList>();
    public DbSet<PriceListItem> PriceListItems => Set<PriceListItem>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<VehicleTypeProduct> VehicleTypeProducts => Set<VehicleTypeProduct>();
    public DbSet<OrderHistory> OrderHistory => Set<OrderHistory>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<Ipg> Ipg => Set<Ipg>();
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentLog> PaymentLogs => Set<PaymentLog>();
    
    public DbSet<WebFile> WebFiles => Set<WebFile>();
    public DbSet<WebPage> WebPages => Set<WebPage>();
    
    public DbSet<AuditEntity> Audits => Set<AuditEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditSaveChangesInterceptor);
        optionsBuilder.AddInterceptors(_softDeleteSaveChangeInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}