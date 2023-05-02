using KavirTire.Shop.Domain.Invoices;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration: BaseEntityTypeConfiguration<Invoice>
{
    public override void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasMany(p => p.InvoiceItems)
            .WithOne()
            .HasForeignKey(i => i.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(a => a.TotalPostCost)
            .HasColumnType("decimal(18,4)"); 
        
        builder.Property(a => a.TotalInventoryItemCost)
            .HasColumnType("decimal(18,4)");
        
        base.Configure(builder);
    }
}