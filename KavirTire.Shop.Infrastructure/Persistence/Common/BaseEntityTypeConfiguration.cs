using KavirTire.Shop.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KavirTire.Shop.Infrastructure.Persistence.Common;

public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        if (typeof(T).GetInterfaces().Any(i => i == typeof(ISoftDeletableEntity)))
        {
            builder.HasQueryFilter(t => ((ISoftDeletableEntity)t).IsDeleted == false);
        }
    }
}