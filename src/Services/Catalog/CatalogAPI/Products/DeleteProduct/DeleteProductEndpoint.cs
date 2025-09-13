﻿namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);
public class DeleteProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
		{
			var result = await sender.Send(new DeleteProductCommand(Id));
			var response = result.Adapt<DeleteProductResponse>();
			return Results.Ok(response);
		})
		.WithName("Delete Product")
		.Produces<DeleteProductResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Delete Product")
		.WithDescription("Delete Product");
	}
}
