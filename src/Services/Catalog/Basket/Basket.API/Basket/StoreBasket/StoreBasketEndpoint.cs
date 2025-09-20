namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
		{
			var command = request.Adapt<StoreBasketCommand>();
			var response = await sender.Send(command);
			return Results.Created($"/basket/{request.Cart.UserName}", response.Adapt<StoreBasketResponse>());
		});
	}
}
