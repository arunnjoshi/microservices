namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
	public StoreBasketCommandValidator()
	{
		RuleFor(x => x.Cart)
			.NotNull()
			.WithMessage("Cart can not be null.");

		RuleFor(x => x.Cart.UserName)
			.NotNull()
			.NotEmpty()
			.WithMessage("UserName is required.");
	}
}

public class StoreBasketHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
	public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
	{
		ShoppingCart cart = request.Cart;
		await basketRepository.StoreBasket(cart, cancellationToken);
		return new StoreBasketResult(request.Cart.UserName);
	}
}
