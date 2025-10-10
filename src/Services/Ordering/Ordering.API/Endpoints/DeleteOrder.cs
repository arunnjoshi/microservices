﻿namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/orders/{id:guid}", async (Guid id, ISender sender) =>
		{
			var command = new DeleteOrderCommand(id);
			var result = await sender.Send(command);
			var response = result.Adapt<DeleteOrderResponse>();
			return Results.Ok(response);
		})
		.WithName("DeleteOrder")
		.Produces<CreateOrderResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Delete Order")
		.WithDescription("Delete Order");
	}
}
