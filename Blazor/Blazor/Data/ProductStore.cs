namespace Blazor.Data;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? SKU { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; } = true;
}

public static class ProductStore
{
    public static List<Product> Products { get; } = new()
    {
        //new Product { ProductId = 1, Name = "Product 1", Description = "Description 1", SKU = "SKU001", Price = 9.99m, StockQuantity = 100 },
        //new Product { ProductId = 2, Name = "Product 2", Description = "Description 2", SKU = "SKU002", Price = 19.99m, StockQuantity = 50 },
        //new Product { ProductId = 3, Name = "Product 3", Description = "Description 3", SKU = "SKU003", Price = 29.99m, StockQuantity = 75 }
    };
}