﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Audit;

public class AuditEntityConfiguration : IEntityTypeConfiguration<AuditEntity>
{
    public AuditEntityConfiguration()
    {
    }

    public void Configure(EntityTypeBuilder<AuditEntity> builder)
    {
        #region Configuration
        builder.ToTable("Audits");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        #endregion
    }
}