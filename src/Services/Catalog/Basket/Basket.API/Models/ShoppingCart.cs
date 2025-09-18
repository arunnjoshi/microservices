﻿namespace Basket.API.Models;

public class ShoppingCart
{
	public string UserName { get; set; } = default!;
	public List<ShoppingCartItem> Items { get; set; } = default!;
	public decimal TotalPrice { get; set; } = default!;
	public ShoppingCart(string userName)
	{
		UserName = userName;
	}

	// for mapping
	public ShoppingCart()
	{
		
	}
}
