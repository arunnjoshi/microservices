namespace Ordering.Application.Orders.Queries.GetOrdersByName;
public class GetOrdersByNameHandler(IApplicationDbContext _context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
	public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
	{
		var orders = await _context.Orders.Include(x => x.OrderItems)
											.AsNoTracking()
											.Where(x => x.OrderName.Value.Contains(request.Name))
											.OrderBy(x => x.OrderName)
											.ToListAsync(cancellationToken);
		return new GetOrdersByNameResult(orders.ToOrderDtoList());
	}
}
