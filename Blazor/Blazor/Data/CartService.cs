using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Data;

public sealed class CartItem
{
    public Product Product { get; init; } = default!;
    public int Quantity { get; set; }
}

public sealed class CartService
{
    private readonly List<CartItem> _items = new();

    public event Action? OnChange;

    public IReadOnlyList<CartItem> Items => _items;

    public void Add(Product product, int quantity = 1)
    {
        if (product is null) return;
        var item = _items.FirstOrDefault(i => i.Product.Id == product.Id);
        if (item is null)
        {
            _items.Add(new CartItem { Product = product, Quantity = Math.Max(1, quantity) });
        }
        else
        {
            item.Quantity += Math.Max(1, quantity);
        }
        NotifyStateChanged();
    }

    public void Remove(int productId)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item is not null)
        {
            _items.Remove(item);
            NotifyStateChanged();
        }
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item is not null)
        {
            if (quantity <= 0) _items.Remove(item);
            else item.Quantity = quantity;
            NotifyStateChanged();
        }
    }

    public void Clear()
    {
        if (_items.Count == 0) return;
        _items.Clear();
        NotifyStateChanged();
    }

    public decimal Total() => _items.Sum(i => i.Product.Price * i.Quantity);

    private void NotifyStateChanged() => OnChange?.Invoke();
}