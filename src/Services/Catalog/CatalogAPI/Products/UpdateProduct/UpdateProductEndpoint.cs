namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest
(
	Guid Id,
	string Name,
	List<string> Category,
	string Description,
	string ImageFile,
	decimal Price
);
public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/products", async (UpdateProductRequest req, ISender sender) =>
		{
			var command = req.Adapt<UpdateProductCommand>();
			var result = await sender.Send(command);
			var response = result.Adapt<UpdateProductResponse>();
			return Results.Ok(response);
		})
		.WithName("update Product")
		.WithDescription("update product")
		.WithDisplayName("update product")
		.Produces<UpdateProductResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("update product");
	}
}
