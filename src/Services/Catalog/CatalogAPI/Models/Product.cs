namespace CatalogAPI.Models;

public class Product
{
	public Guid Id { get; set; }
	public string Name { get; set; }  = null!;
	public string Description { get; set; }  = null!;
	public string ImageFile { get; set; }  = null!;
	public decimal Price { get; set; }  = default;
	public List<string> Category { get; set; }  = [];
}
