namespace Ordering.Application.Dtos;

public record PaymentDto
(
	string CardNumber ,
	string CardName ,
	string Expiration ,
	string CVV ,
	int PaymentMethod 
);