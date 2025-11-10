using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> _logger) : INotificationHandler<OrderCreatedEvent>
{
	public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Domain Event handle: {DomainEvent}", notification.GetType());
		return Task.CompletedTask;
	}
}
