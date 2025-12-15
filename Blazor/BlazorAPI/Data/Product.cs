namespace BlazorAPI.Data;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? SKU { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}