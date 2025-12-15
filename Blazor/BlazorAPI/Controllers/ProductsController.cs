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
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        try
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Database error: {ex.Message}");
        }
    }
}