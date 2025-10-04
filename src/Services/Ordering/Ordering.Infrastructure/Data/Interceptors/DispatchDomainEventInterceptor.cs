using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventInterceptor(IMediator mediator) : SaveChangesInterceptor
{
	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();
		return base.SavingChanges(eventData, result);
	}

	public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		await DispatchDomainEvent(eventData.Context);
		return await base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private async Task DispatchDomainEvent(DbContext context)
	{
		if (context is null) return;
		var entitiesWithEvents = context.ChangeTracker.Entries<IAggregate>()
			.Select(e => e.Entity)
			.Where(e => e.DomainEvents.Any())
			.ToArray();

		var domainEvents = entitiesWithEvents
			.SelectMany(e => e.DomainEvents)
			.ToArray();

		foreach (var entity in entitiesWithEvents)
		{
			entity.ClearDomainEvents();
		}

		foreach(var domainEvent in domainEvents)
		{
			await mediator.Publish(domainEvent);
		}	
	}
}
