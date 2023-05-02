using KavirTire.Shop.Domain.PriceLists.Entities;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Configurations;


public class PriceListItemConfiguration : BaseEntityTypeConfiguration<PriceListItem>
{
    public override void Configure(EntityTypeBuilder<PriceListItem> builder)
    {
        builder.Property(a => a.Amount)
            .HasColumnType("decimal(18,4)");
        base.Configure(builder);
    }
}

