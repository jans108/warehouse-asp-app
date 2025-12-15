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

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            Console.WriteLine("ProductService: Starting GetProductsAsync");
            Console.WriteLine($"ProductService: Using base address: {_httpClient.BaseAddress}");
            
            var response = await _httpClient.GetAsync("api/products");
            
            Console.WriteLine($"ProductService: Response status code: {response.StatusCode}");
            Console.WriteLine($"ProductService: Response content type: {response.Content.Headers.ContentType}");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"ProductService: API Error {response.StatusCode} - {errorContent}");
                return new List<Product>();
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"ProductService: Raw response content: {content}");
            Console.WriteLine($"ProductService: Content length: {content.Length}");
            
            // Check if content is HTML (error page)
            if (content.TrimStart().StartsWith("<"))
            {
                Console.WriteLine("ProductService: Response is HTML, not JSON!");
                Console.WriteLine($"ProductService: HTML content: {content}");
                return new List<Product>();
            }
            
            var products = JsonSerializer.Deserialize<List<Product>>(content, JsonOptions) ?? new();
            Console.WriteLine($"ProductService: Successfully deserialized {products.Count} products");
            
            return products;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ProductService: Exception occurred: {ex.Message}");
            Console.WriteLine($"ProductService: Exception type: {ex.GetType().Name}");
            Console.WriteLine($"ProductService: Stack Trace: {ex.StackTrace}");
            return new List<Product>();
        }
    }
}