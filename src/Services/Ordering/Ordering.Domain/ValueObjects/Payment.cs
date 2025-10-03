namespace Ordering.Domain.ValueObjects;

public record Payment
{
	public string CardNumber { get; } = default!;
	public string CardName { get; } = default!;
	public string Expiration { get; } = default!;
	public string CVV { get; } = default!;
	public int PaymentMethod { get; } = default!;

	protected Payment()
	{
		
	}

	private Payment(string cardNumber, string cardName, string expirationDate, string cvv, int paymentMethod)
	{
		CardNumber = cardNumber;
		CardName = cardName;
		Expiration = expirationDate;
		PaymentMethod = paymentMethod;
		CVV = cvv;
	}
	public static Payment Of(string cardNumber, string cardName, string expirationDate, string cvv, int paymentMethod)
	{
		ArgumentException.ThrowIfNullOrEmpty(cardNumber);
		ArgumentException.ThrowIfNullOrEmpty(cardName);
		ArgumentException.ThrowIfNullOrEmpty(cvv);
		ArgumentOutOfRangeException.ThrowIfLessThan(cvv.Length, 3);
		return new Payment(cardNumber, cardName, expirationDate, cvv, paymentMethod);
	}
}
