namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext _context) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
	public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
	{
		var customerId = CustomerId.Of(request.CustomerId);
		var orders = await _context.Orders.Include(x => x.OrderItems)
											.AsNoTracking()
											.Where(x => x.CustomerId == customerId)
											.OrderBy(x => x.OrderName.Value)
											.ToListAsync(cancellationToken);
		return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
	}
}
