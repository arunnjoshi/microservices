namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(bool IsSuccess);

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

public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
	public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
	{
		ShoppingCart cart = request.Cart;

		return new StoreBasketResult(true);
	}
}
