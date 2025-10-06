namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext _context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
	public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
	{
		var orderId = OrderId.Of(command.OrderId);
		var order = await _context.Orders.FindAsync(orderId,cancellationToken) ?? throw new OrderNotFoundException(command.OrderId);
		_context.Orders.Remove(order);
		await _context.SaveChangesAsync(cancellationToken);
		return new DeleteOrderResult(true);
	}
}
