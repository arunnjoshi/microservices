using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> products);
public class GetProductByCategoryEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
		{
			var req = new GetProductByCategoryQuery(category);
			var result = await sender.Send(req);
			var response = result.Adapt<GetProductByCategoryResponse>();
			return Results.Ok(response);
		})
		.WithName("Get products by category")
		.Produces<CreateProductResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get products by category")
		.WithDescription("Get products by category");
	}
}
