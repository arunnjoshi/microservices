namespace Ordering.API.Endpoints;

public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/Orders/{orderName}", async (string orderName,ISender sender) =>
		{
			var query = new GetOrdersByNameQuery(orderName);
			var result = await sender.Send(query);
			var response = result.Adapt<GetOrderByNameResponse>();
			return Results.Ok(response);
		})
		.WithName("GetOrderByName")
		.Produces<CreateOrderResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Order by Name")
		.WithDescription("Get Order by Name");
	}
}
