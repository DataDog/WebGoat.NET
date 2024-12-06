namespace WebGoat.NET.ViewModels;

public class AccountViewModel
{
    public required string? Username { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CompanyName { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string PostalCode { get; set; }
}