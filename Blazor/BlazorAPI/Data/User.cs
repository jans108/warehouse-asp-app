namespace BlazorAPI.Data;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}