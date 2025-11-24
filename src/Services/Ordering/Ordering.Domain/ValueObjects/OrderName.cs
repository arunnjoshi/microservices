namespace Ordering.Domain.ValueObjects;

public record OrderName
{
	private const int DefaultNameLength = 5;
	public string Value { get; }

	private OrderName(string value) => Value = value;

	public static OrderName Of(string value)
	{
		ArgumentNullException.ThrowIfNull(value);
		//ArgumentOutOfRangeException.ThrowIfLessThan(value.Length, DefaultNameLength);
		return new OrderName(value);
	}
}
