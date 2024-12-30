// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.Data;
using WebGoat.NET.Models;
using WebGoat.NET.ViewModels;

namespace WebGoat.NET.Controllers;

public class CartController(IHttpContextAccessor httpContextAccessor, ProductRepository productRepository) : Controller
{
    private const string CartKey = "Cart";

    public Cart GetCart()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            return new Cart();
        }

        httpContextAccessor.HttpContext.Session.TryGetValue(CartKey, out var cartBytes);
        return cartBytes == null ? new Cart() : Cart.Deserialize(productRepository, cartBytes);
    }
    
    public static void RemoveCart(HttpContext httpContext)
    {
        httpContext.Session.Remove(CartKey);
    }
    
    public int GetCartItemCount()
    {
        return GetCart().Products.Sum(x => x.Quantity);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View(new CartViewModel { Cart = GetCart() });
    }

    [HttpPost]
    public IActionResult UpdateCart([FromForm] int productId, int quantity, string action = "increase")
    {
        if (action != "increase" && action != "decrease")
        {
            return StatusCode(400);
        }
        
        var product = productRepository.GetProductById(productId);
        if (product == null)
        {
            return RedirectToAction("Index");
        }
        
        var cart = GetCart();

        if (action == "increase")
        {
            cart.AddProduct(product, quantity);
        }
        else
        {
            cart.RemoveProduct(product, quantity);
        }

        var serializedCart = cart.Serialize();
        HttpContext.Session.Set("Cart", serializedCart);

        return RedirectToAction("Index");
    }
}