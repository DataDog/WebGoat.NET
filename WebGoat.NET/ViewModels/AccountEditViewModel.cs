using System.ComponentModel.DataAnnotations;

namespace WebGoat.NET.ViewModels;

public class AccountEditViewModel
{
    [Required(ErrorMessage = "First Name is required")]
    [Display(Name = "First Name")]
    public required string FirstName { get; init; }

    [Required(ErrorMessage = "Last Name is required")]
    [Display(Name = "Last Name")]
    public required string LastName { get; init; }

    [Required(ErrorMessage = "Company Name is required")]
    [Display(Name = "Company Name")]
    public required string CompanyName { get; init; }

    [Required(ErrorMessage = "Address is required")]
    [Display(Name = "Address")]
    public required string Address { get; init; }

    [Required(ErrorMessage = "City is required")]
    [Display(Name = "City")]
    public required string City { get; init; }

    [Required(ErrorMessage = "Country is required")]
    [Display(Name = "Country")]
    public required string Country { get; init; }

    [Required(ErrorMessage = "Postal Code is required")]
    [Display(Name = "Postal Code")]
    public required string PostalCode { get; init; }
}