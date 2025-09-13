﻿using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);


public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
	public DeleteProductCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotNull()
			.NotEmpty()
			.WithMessage("Id is required.");
	}
}
public class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
	public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		_ = await session.LoadAsync<Product>(request.Id, cancellationToken) ?? throw new ProductNotFoundException(request.Id);
		session.Delete<Product>(request.Id);
		await session.SaveChangesAsync(cancellationToken);
		return new DeleteProductResult(true);
	}
}
