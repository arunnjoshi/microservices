namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext _context) : IRequestHandler<GetOrdersQuery, GetOrdersResult>
{
	public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
	{
		var pageSize = request.PaginationRequest.PageSize;
		var pageIndex = request.PaginationRequest.PageIndex;
		var count = await _context.Orders.LongCountAsync(cancellationToken);

		var orders = await _context.Orders.Include(x => x.OrderItems)
											.OrderBy(x => x.OrderName.Value)
											.Skip(pageIndex * pageSize)
											.Take(pageSize)
											.ToListAsync(cancellationToken);

		return new GetOrdersResult(new BuildingBlocks.Pagination.PaginatedResult<OrderDto>(pageIndex,pageSize,count,orders.ToOrderDtoList()));
	}
}
