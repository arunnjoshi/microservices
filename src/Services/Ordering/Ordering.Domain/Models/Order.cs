namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
	private readonly List<OrderItem> _orderItems = [];
	public IReadOnlyList<OrderItem> orderItems => _orderItems.AsReadOnly();

	public string CustomerId { get; private set; } = default!;
	public string OrderName { get; private set; } = default!;
	public Address ShippingAddress { get; private set; } = default!;
	public Address BillingAddress { get; private set; } = default!;
	public Payment Payment { get; private set; } = default!;
	public OrderStatus Status { get; private set; } = OrderStatus.Pending!;
	public decimal TotalPrice
	{
		get => orderItems.Sum(x => x.Quantity * x.Price);
		private set { }
	}
}