using System.ComponentModel.DataAnnotations;

namespace WebGoat.NET.Models
{
    public class RegisterViewModel
    {
        [Required]
        public required string Username { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public required string ConfirmPassword { get; init; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; init; }
        
        // first name
        [Required]
        [Display(Name = "First Name")]
        public required string FirstName { get; init; }
        
        // last name
        [Required]
        [Display(Name = "Last Name")]
        public required string LastName { get; init; }
        
        [Required]
        [Display(Name = "Company Name")]
        public required string CompanyName { get; init; }
        
        [Required]
        [Display(Name = "Address")]
        public required string Address { get; init; }
        
        [Required]
        [Display(Name = "City")]
        public required string City { get; init; }
        
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public required string PostalCode { get; init; }
        
        [Required]
        public required string Country { get; init; }
    }
}