using BuildingBlocks.CQRS;
using CatalogAPI.Models;

namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand
(
	string Name,
	List<string> Category,
	string Description,
	string ImageFile,
	decimal Price
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		//create product entity from command object
		//Save to DB
		// return
		var product = new Product
		{
			Name = command.Name,
			Categories = command.Category,
			Description = command.Description,
			ImageFile = command.ImageFile,
			Price = command.Price,
		};
		return new CreateProductResult(Guid.NewGuid());
	}
}
