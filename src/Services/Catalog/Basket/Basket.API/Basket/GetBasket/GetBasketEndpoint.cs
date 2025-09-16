namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
		{
			var result = await sender.Send(new GetBasketQuery(userName));
			return result.Adapt<GetBasketResponse>();
		})
		.WithName("GetBasketByUserName")
		.Produces<GetBasketResponse>()
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Basket by username")
		.WithDescription("Get Basket by username");
	}
}
