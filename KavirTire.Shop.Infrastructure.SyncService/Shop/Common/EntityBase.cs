using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Common
{

    public abstract class EntityBase<TId> 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TId Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<Guid>
    {
    }
}