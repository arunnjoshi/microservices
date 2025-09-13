namespace CatalogAPI.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> products);

public class GetProductsEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products", async (ISender sender) =>
		{
			var result = await sender.Send(new GetProductQuery());
			var response = result.Adapt<GetProductsResponse>();
			return Results.Ok(response);
		})
		.WithName("GetProducts")
		.WithDescription("get products")
		.WithDisplayName("get products")
		.Produces<GetProductsResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("get products");
	}
}
