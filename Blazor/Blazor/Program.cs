using Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.Data;
using Blazor.Services;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to use the API base address
builder.Services.AddScoped(sp => 
{
    var apiBaseAddress = "https://localhost:7032/"; // API URL
    var client = new HttpClient { BaseAddress = new Uri(apiBaseAddress) };
    return client;
});

builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
