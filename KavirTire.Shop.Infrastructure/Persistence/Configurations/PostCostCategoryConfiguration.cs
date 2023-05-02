using KavirTire.Shop.Domain.PostCostCategories;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Configurations;


public class PostCostCategoryConfiguration : BaseEntityTypeConfiguration<PostCostCategory>
{
    public override void Configure(EntityTypeBuilder<PostCostCategory> builder)
    {
        builder.Property(a => a.TirePostCost)
            .HasColumnType("decimal(18,4)");
        base.Configure(builder);
    }
}