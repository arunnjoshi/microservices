namespace CatalogAPI.Products.GetProducts;
public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> products);

public class GetProductsEndPoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
		{
			var query = request.Adapt<GetProductQuery>();
			var result = await sender.Send(query);
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
