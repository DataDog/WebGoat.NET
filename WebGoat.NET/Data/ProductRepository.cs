// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using WebGoat.NET.Models;

namespace WebGoat.NET.Data;

public class ProductRepository(ApplicationDbContext context)
{
    public Product? GetProductById(int id)
    {
        return context.Products.Find(id);
    }
    
    public List<Product> GetProducts()
    {
        return context.Products.ToList();
    }
    
    public List<Product> GetFeaturedProducts()
    {
        var ids = new List<int> { 1, 2 };
        return context.Products.Where(p => ids.Contains(p.Id)).ToList();
    }
}