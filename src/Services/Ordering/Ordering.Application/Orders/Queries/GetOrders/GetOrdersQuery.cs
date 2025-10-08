using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IRequest<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
