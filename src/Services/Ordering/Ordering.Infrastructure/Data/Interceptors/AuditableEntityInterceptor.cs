﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		UpdateEntities(eventData.Context);


		return base.SavingChanges(eventData, result);
	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		UpdateEntities(eventData.Context);
		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private void UpdateEntities(DbContext? dbContext)
	{
		if (dbContext is null)
			return;

		foreach (var entry in dbContext.ChangeTracker.Entries<IEntity>())
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedBy = "arun";
				entry.Entity.CreatedAt = DateTime.UtcNow;
			}
			else if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
			{
				entry.Entity.LastModifiedBy = "arun";
				entry.Entity.LastModifiedAt = DateTime.UtcNow;
			}
		}
	}
}

public static class EntityEntryExtensions
{
	public static bool HasChangedOwnedEntities(this EntityEntry entry)
	{
		return entry.References.Any(r => r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned()
					&& (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
	}
}
