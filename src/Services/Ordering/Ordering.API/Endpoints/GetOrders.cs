namespace Ordering.API.Endpoints;

public record GetOrderResponse(PaginatedResult<OrderDto> Orders);


public class GetOrders : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/Orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
		{
			var query = new GetOrdersQuery(request);
			var result = await sender.Send(query);
			var response = result.Adapt<GetOrderResponse>();
			return Results.Ok(response);
		})
		.WithName("GetOrderPagination")
		.Produces<CreateOrderResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Order Pagination")
		.WithDescription("Get Order Pagination");
	}
}
