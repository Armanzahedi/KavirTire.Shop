﻿using KavirTire.Shop.Application.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using KavirTire.Shop.Domain.Common;

namespace KavirTire.Shop.Infrastructure.Persistence.Audit.Interceptors;

public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTime;

    public AuditSaveChangesInterceptor(
        IDateTimeProvider dateTime)
    {
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAuditPolicy(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ApplyAuditPolicy(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void ApplyAuditPolicy(DbContext? context)
    {
        if (context == null) return;
        
        foreach (var entry in context.ChangeTracker.Entries().Where(entry => entry.ShouldBeAudited()).ToList())
        {
            context.Attach(new AuditEntity(entry));
        }
        
        foreach (var entry in context.ChangeTracker.Entries<AuditableEntityBase>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "";
                entry.Entity.CreatedOn = _dateTime.UtcNow;
            } 

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = "";
                entry.Entity.LastModifiedOn = _dateTime.UtcNow;
            }
        }
        
    }
    
}


