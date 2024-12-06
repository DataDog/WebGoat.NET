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