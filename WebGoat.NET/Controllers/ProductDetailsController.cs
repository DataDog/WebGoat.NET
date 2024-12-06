using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.Data;
using WebGoat.NET.ViewModels;

namespace WebGoat.NET.Controllers;

[AllowAnonymous]
[Route("/[controller]")]
public class ProductDetailsController(ProductRepository productRepository) : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet("{id}")]
    public IActionResult Index(int id)
    {
        var product = productRepository.GetProductById(id);
        if (product == null)
        {
            return RedirectToAction("Index", "Home");
        }
        
        return View(new ProductDetailsViewModel() { Product = product });
    }
}