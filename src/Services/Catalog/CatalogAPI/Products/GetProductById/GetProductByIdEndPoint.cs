namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdResponse(Product product);
public class GetProductByIdEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
		{
			var result = await sender.Send(new GetProductByIdQuery(id));
			var response = result.Adapt<GetProductByIdResponse>();
			return Results.Ok(response);
		})
		.WithName("GetProductById")
		.WithDescription("get product by id")
		.WithDisplayName("get product by id")
		.Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("get product by id");
	}
}
