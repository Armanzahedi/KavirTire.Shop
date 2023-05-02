using KavirTire.Shop.Domain.Invoices.Entities;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Configurations;

public class InvoiceItemConfiguration : BaseEntityTypeConfiguration<InvoiceItem>
{
    public override void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.Property(a => a.Amount)
            .HasColumnType("decimal(18,4)");
        builder.Property(a => a.PostCost)
            .HasColumnType("decimal(18,4)");
        base.Configure(builder);
    }
}
