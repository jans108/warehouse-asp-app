using BlazorAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly WarehouseDbContext _context;

    public ProductsController(WarehouseDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? search)
    {
        try
        {
            var query = _context.Products.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchLower = search.ToLower();
                query = query.Where(p => 
                    p.Name.ToLower().Contains(searchLower) ||
                    (p.Description != null && p.Description.ToLower().Contains(searchLower)) ||
                    (p.SKU != null && p.SKU.ToLower().Contains(searchLower))
                );
            }

            var products = await query.ToListAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId }, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creating product: {ex.Message}");
        }
    }
}