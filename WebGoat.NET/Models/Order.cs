using System.ComponentModel.DataAnnotations;

namespace WebGoat.NET.Models;

public class Order
{
    [Key]
    public int Id { get; init; }

    public required string UserId { get; init; }
    
    public required List<OrderItem> Products { get; init; } = [];

    public required string CreditCardNumber { get; init; }

    public required OrderStatus Status { get; init; }
    
    public DateTime OrderDate { get; init; }
    
    public required string CompanyName { get; init; }
    
    public required string Address { get; init; }
    
    public required string City { get; init; }
    
    public required string Country { get; init; }
    
    public required string PostalCode { get; init; }
    
    public required string ShippingProvider { get; init; }
    
    public required string TrackingNumber { get; init; }
    
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
    
    public decimal TotalPrice
    {
        get
        {
            return Products.Sum(p => p.Product.Price * p.Quantity);
        }
    }
}