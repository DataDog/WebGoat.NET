using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.Data;
using WebGoat.NET.Models;
using WebGoat.NET.ViewModels;

namespace WebGoat.NET.Controllers;

public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CustomerRepository customerRepository)
    : Controller
{
    public string? GetUserId()
    {
        return userManager.GetUserId(User);
    }
    
    public IActionResult Index()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login");
        }
        
        var customer = customerRepository.GetCurrentCustomer(User);
        if (customer == null)
        {
            return RedirectToAction("Index", "Home");
        }
        
        return View(new AccountViewModel
        {
            Username = User.Identity.Name,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            CompanyName = customer.CompanyName,
            Address = customer.Address,
            City = customer.City,
            Country = customer.Country,
            PostalCode = customer.PostalCode
        });
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Username };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                customerRepository.CreateUser(user, model.Email, model.FirstName, model.LastName, model.CompanyName, model.Address, model.City, model.Country, model.PostalCode);

                await signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        CartController.RemoveCart(HttpContext);
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    public IActionResult Edit()
    {
        var customer = customerRepository.GetCurrentCustomer(User);
        if (customer == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var model = new AccountEditViewModel
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            CompanyName = customer.CompanyName,
            Address = customer.Address,
            City = customer.City,
            Country = customer.Country,
            PostalCode = customer.PostalCode
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AccountEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var customer = customerRepository.GetCurrentCustomer(User);
            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Update all the customer attributes
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.CompanyName = model.CompanyName;
            customer.Address = model.Address;
            customer.City = model.City;
            customer.Country = model.Country;
            customer.PostalCode = model.PostalCode;
            customerRepository.UpdateCustomer(customer);
            
            return RedirectToAction("Index");
        }

        // If the model state is not valid, redisplay the form with validation errors
        return View(model);
    }

}