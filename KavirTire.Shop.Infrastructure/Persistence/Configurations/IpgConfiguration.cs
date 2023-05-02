using KavirTire.Shop.Domain.IPGs;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Configurations;

public class IpgConfiguration : BaseEntityTypeConfiguration<Ipg>
{
    public override void Configure(EntityTypeBuilder<Ipg> builder)
    {
        builder
            .HasMany(p => p.BankAccounts)
            .WithOne()
            .HasForeignKey(i => i.IpgId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}