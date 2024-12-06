using Microsoft.EntityFrameworkCore;
using WebGoat.NET.Models;

namespace WebGoat.NET.Data;

public class OrderRepository(ApplicationDbContext context)
{
    public Order? GetOrderById(int id)
    {
        return context.Orders.Find(id);
    }
    
    public List<Order> GetOrders()
    {
        return context.Orders.ToList();
    }
    
    public void CreateOrder(Order order)
    {
        var sql = "INSERT INTO Orders (" +
            "UserId, CreditCardNumber, Status, OrderDate, CompanyName, Address, City, Country, PostalCode, ShippingProvider, TrackingNumber" +
            ") VALUES (" +
            $"'{order.UserId}','{order.CreditCardNumber}','{order.Status}','{order.OrderDate:yyyy-MM-dd}','{order.CompanyName}','{order.Address}'," +
            $"'{order.City}','{order.Country}','{order.PostalCode}','{order.ShippingProvider}','{order.TrackingNumber}')";

        using var command = context.Database.GetDbConnection().CreateCommand();
        command.CommandText = sql;
        context.Database.OpenConnection();
        command.ExecuteNonQuery();
    }
}