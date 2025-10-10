namespace Ordering.API.Endpoints;
public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/Orders/Customer/{CustomerId}", async (Guid customerId, ISender sender) =>
		{
			var query = new GetOrdersByCustomerQuery(customerId);
			var result = await sender.Send(query);
			var response = result.Adapt<GetOrderByCustomerResponse>();
			return Results.Ok(response);
		})
		.WithName("GetOrderByCustomerID")
		.Produces<CreateOrderResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Order by CustomerId")
		.WithDescription("Get Order by CustomerId");
	}
}
