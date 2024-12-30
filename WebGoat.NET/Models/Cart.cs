// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using System.Text.Json;
using WebGoat.NET.Data;

namespace WebGoat.NET.Models;

public class Cart
{
    public List<OrderItem> Products { get; private init; } = [];
    
    public decimal TotalPrice { get; private set; }

    public void AddProduct(Product product, int quantity)
    {
        var foundItem = Products.FirstOrDefault(p => p.Id == product.Id);
        if (foundItem != null)
        {
            foundItem.Quantity += quantity;
        }
        else
        {
            var orderItem = CreateOrderItem(product, quantity);
            Products.Add(orderItem);
        }

        TotalPrice += product.Price * quantity;
    }
    
    public void RemoveProduct(Product product, int quantity)
    {
        var foundItem = Products.FirstOrDefault(p => p.Id == product.Id);
        if (foundItem == null)
        {
            return;
        }
        
        foundItem.Quantity -= quantity;
        if (foundItem.Quantity <= 0)
        {
            Products.Remove(foundItem);
        }
        
        TotalPrice = Products.Sum(p => p.Product.Price * p.Quantity);
    }
    
    public static Cart Deserialize(ProductRepository productRepository, byte[] cartData)
    {
        var savedCart = JsonSerializer.Deserialize<SavedCart>(cartData);
        List<OrderItem> products = [];
        decimal totalPrice = 0;
        for (var i = 0; i < savedCart.ProductIds.Count; i++)
        {
            var productId = savedCart.ProductIds[i];
            var product = productRepository.GetProductById(productId);
            if (product == null)
            {
                continue;
            }
            
            var quantity = savedCart.Quantities[i];
            products.Add(new OrderItem
            {
                Id = productId,
                Product = product,
                Quantity = quantity
            });
            
            totalPrice += product.Price * quantity;
        }

        return new Cart { Products = products, TotalPrice = totalPrice };
    }
    
    public byte[] Serialize()
    {
        var cartSaved = new SavedCart
        {
            ProductIds = Products.Select(p => p.Id).ToList(),
            Quantities = Products.Select(p => p.Quantity).ToList()
        };
        
        return JsonSerializer.SerializeToUtf8Bytes(cartSaved);
    }

    private static OrderItem CreateOrderItem(Product product, int quantity)
    {
        return new OrderItem
        {
            Id = product.Id,
            Product = product,
            Quantity = quantity
        };
    }
    
    private struct SavedCart
    {
        public List<int> ProductIds { get; init; }
        public List<int> Quantities { get; init; }
    }
}