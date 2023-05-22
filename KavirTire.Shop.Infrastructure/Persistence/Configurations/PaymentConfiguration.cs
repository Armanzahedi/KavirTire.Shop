using KavirTire.Shop.Domain.Payments;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration: BaseEntityTypeConfiguration<Domain.Payments.Payment>
{
    public override void Configure(EntityTypeBuilder<Domain.Payments.Payment> builder)
    {
        builder.HasMany(p => p.PaymentLogs)
            .WithOne()
            .HasForeignKey(i => i.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(a => a.Amount)
            .HasColumnType("decimal(18,4)");
        base.Configure(builder);
    }
}