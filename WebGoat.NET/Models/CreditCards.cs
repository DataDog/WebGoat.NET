namespace WebGoat.NET.Models;

public class CreditCards
{
    public required int Id { get; set; }
    
    public required string Number { get; set; }
    
    public required string Holder { get; set; }
    
    public required DateTime ExpirationDate { get; set; }
    
    public required string Cvv { get; set; }
}