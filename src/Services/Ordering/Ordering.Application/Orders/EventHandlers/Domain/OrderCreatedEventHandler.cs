using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> _logger, 
										IPublishEndpoint publishEndpoint,
										IFeatureManager featureManager) : INotificationHandler<OrderCreatedEvent>
{
	public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Domain Event handle: {DomainEvent}", notification.GetType());
		if(!await featureManager.IsEnabledAsync("OrderFulfillment"))
		{
			var orderCreatedIntegrationEvent = notification.order.ToOrderDto();
			await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
		}
	}
}
