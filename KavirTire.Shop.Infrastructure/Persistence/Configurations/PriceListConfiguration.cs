using KavirTire.Shop.Domain.PriceLists;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Configurations;

public class PriceListConfiguration: BaseEntityTypeConfiguration<PriceList>
{
    public override void Configure(EntityTypeBuilder<PriceList> builder)
    {
        builder.HasMany(p => p.PriceListItems)
            .WithOne()
            .HasForeignKey(i => i.PriceListId)
            .OnDelete(DeleteBehavior.Cascade);
        base.Configure(builder);
    }
}