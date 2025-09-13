using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand
(
	Guid Id,
	string Name,
	List<string> Category,
	string Description,
	string ImageFile,
	decimal Price
) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
	public UpdateProductCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotNull()
			.NotEmpty()
			.WithMessage("Id is required.");

		RuleFor(x => x.Name)
			.NotNull()
			.NotEmpty()
			.WithMessage("Name is required.");

		RuleFor(x => x.Category)
			.NotNull()
			.NotEmpty()
			.WithMessage("Category is required.");

		RuleFor(x => x.ImageFile)
			.NotNull()
			.NotEmpty()
			.WithMessage("ImageFile is required.");

		RuleFor(x => x.Price)
			.NotNull()
			.NotEmpty()
			.WithMessage("Price is required.")
			.GreaterThan(0)
			.WithMessage("Price must be greater than 0.");
	}
}
public class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
	public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
	{
		var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
		if (product is null)
		{
			throw new ProductNotFoundException(request.Id);
		}
		product.Name = request.Name;
		product.Categories = request.Category;
		product.Description = request.Description;
		product.ImageFile = request.ImageFile;
		product.Price = request.Price;

		session.Update(product);
		await session.SaveChangesAsync(cancellationToken);
		return new UpdateProductResult(true);
	}
}
