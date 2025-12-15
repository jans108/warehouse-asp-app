using Microsoft.EntityFrameworkCore;
using BlazorAPI.Data;

namespace BlazorAPI.Data;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.SKU).HasMaxLength(50);
            entity.HasIndex(e => e.SKU).IsUnique();
            entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.HasMany(e => e.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId);
        });

        // Order configuration
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            entity.Property(e => e.OrderEmail).IsRequired().HasMaxLength(255);
            entity.Property(e => e.ShippingAddress).IsRequired().HasMaxLength(500);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.HasMany(e => e.OrderItems).WithOne(oi => oi.Order).HasForeignKey(oi => oi.OrderId);
        });

        // OrderItem configuration
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
            entity.HasOne(e => e.Product).WithMany(p => p.OrderItems).HasForeignKey(e => e.ProductId);
        });

        // Seed initial products
        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "Product 1", Description = "Description 1", SKU = "SKU001", Price = 9.99m, StockQuantity = 100, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Product { ProductId = 2, Name = "Product 2", Description = "Description 2", SKU = "SKU002", Price = 19.99m, StockQuantity = 50, IsActive = true, CreatedAt = DateTime.UtcNow },
            new Product { ProductId = 3, Name = "Product 3", Description = "Description 3", SKU = "SKU003", Price = 29.99m, StockQuantity = 75, IsActive = true, CreatedAt = DateTime.UtcNow }
        );
    }
}