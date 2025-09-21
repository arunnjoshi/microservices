namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
		{
			var request = new DeleteBasketCommand(userName);
			var response = await sender.Send(request);
			return Results.Ok(response.Adapt<DeleteBasketResponse>());
		});
	}
}
