namespace BlazorAPI.Data;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Foreign key and navigation properties
    public Order Order { get; set; } = default!;
    public Product Product { get; set; } = default!;
}