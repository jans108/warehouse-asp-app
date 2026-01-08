using Blazor.Data;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blazor.Services;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions JsonOptions = new() 
    { 
        PropertyNameCaseInsensitive = true 
    };

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync(string? searchTerm = null)
    {
        try
        {
            var url = "api/products";
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                url += $"?search={Uri.EscapeDataString(searchTerm)}";
            }

            Console.WriteLine($"ProductService: Fetching products with URL: {url}");
            
            var response = await _httpClient.GetAsync(url);
            
            Console.WriteLine($"ProductService: Response status code: {response.StatusCode}");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"ProductService: API Error {response.StatusCode} - {errorContent}");
                return new List<Product>();
            }

            var content = await response.Content.ReadAsStringAsync();
            
            if (content.TrimStart().StartsWith("<"))
            {
                Console.WriteLine("ProductService: Response is HTML, not JSON!");
                return new List<Product>();
            }
            
            var products = JsonSerializer.Deserialize<List<Product>>(content, JsonOptions) ?? new();
            Console.WriteLine($"ProductService: Successfully deserialized {products.Count} products");
            
            return products;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ProductService: Exception occurred: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<bool> AddProductAsync(Product product)
    {
        try
        {
            Console.WriteLine($"ProductService: Adding product: {product.Name}");
            
            var response = await _httpClient.PostAsJsonAsync("api/products", product, JsonOptions);
            
            Console.WriteLine($"ProductService: Add product response status: {response.StatusCode}");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"ProductService: Add product failed: {errorContent}");
                return false;
            }

            Console.WriteLine("ProductService: Product added successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ProductService: Error adding product: {ex.Message}");
            return false;
        }
    }
}