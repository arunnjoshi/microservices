using Discount.Grpc;

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

public class StoreBasketHandler(IBasketRepository basketRepository,
								DiscountProtoService.DiscountProtoServiceClient discountProtoService) 
								: ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
	public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
	{
		await DeductDiscount(request.Cart,cancellationToken);
		await basketRepository.StoreBasket(request.Cart, cancellationToken);
		return new StoreBasketResult(request.Cart.UserName);
	}

	public async Task DeductDiscount(ShoppingCart cart,CancellationToken cancellationToken)
	{
		foreach (var item in cart.Items)
		{
			var coupon = await discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
			item.Price -= coupon.Amount;
		}
	}
}
