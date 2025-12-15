namespace BlazorAPI.Data;

public class Order
{
    public int OrderId { get; set; }
    public int? UserId { get; set; }
    public string OrderEmail { get; set; } = "";
    public string ShippingAddress { get; set; } = "";
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";

    // Foreign key and navigation properties
    public User? User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}