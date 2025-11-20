namespace Blazor.Data;

public class Product
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string ShortDescription { get; init; } = "";
    public string Description { get; init; } = "";
    public decimal Price { get; init; }
    public string? ImageUrl { get; init; }
}

public static class ProductStore
{
    public static List<Product> Products { get; } = new()
    {
        new Product { Id = 1, Name = "Product 1", ShortDescription = "Short Description 1", Description = "Description 1.", Price = 9.99m },
        new Product { Id = 2, Name = "Product 2", ShortDescription = "Short Description 2", Description = "Description 2.", Price = 19.99m },
        new Product { Id = 3, Name = "Product 3", ShortDescription = "Short Description 3", Description = "Description 3.", Price = 29.99m }
    };
}