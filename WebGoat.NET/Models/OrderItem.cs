namespace WebGoat.NET.Models;

public class OrderItem
{
    public required int Id { get; set; }
    public required Product Product { get; set; }
    public required int Quantity { get; set; }
}