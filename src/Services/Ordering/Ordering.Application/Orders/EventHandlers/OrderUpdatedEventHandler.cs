using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderCreatedEventHandler> _logger) : INotificationHandler<OrderUpdatedEvent>
{
	public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Domain Event handle: {DomainEvent}", notification.GetType());
		return Task.CompletedTask;
	}
}
