﻿namespace CatalogAPI.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) 
				: IQueryHandler<GetProductQuery, GetProductsResult>
{
	public async Task<GetProductsResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
	{
		var products =  await session.Query<Product>().ToListAsync(cancellationToken);
		return new GetProductsResult(products);
	}
}
