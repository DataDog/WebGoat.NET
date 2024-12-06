using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.Data;
using WebGoat.NET.ViewModels;
using WebGoat.NET.Models;

namespace WebGoat.NET.Controllers;

public class CheckoutController(CartController cartController, CustomerRepository customerRepository, OrderRepository orderRepository) : Controller
{
    private CheckoutViewModel InitializeModel(Customer customer, Cart cart)
    {
        return new CheckoutViewModel
        {
            Cart = cart,
            Customer = customer,
            CompanyName = customer.CompanyName,
            Address = customer.Address,
            City = customer.City,
            PostalCode = customer.PostalCode,
            Country = customer.Country,
            ShippingProvider = "UPS",
            CreditCardNumber = "4242 4242 4242 4242",
            CardHolderName = customer.CompanyName,
            ExpirationMonth = 1,
            ExpirationYear = DateTime.Now.Year + 1,
            Cvv = "424"
        };
    }
    
    public IActionResult Index()
    {
        if (User.Identity is not { IsAuthenticated: true })
        {
            return RedirectToAction("Login", "Account");
        }

        var customer = customerRepository.GetCurrentCustomer(User);
        if (customer == null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(InitializeModel(customer, cartController.GetCart()));
    }

    [HttpPost]
    public IActionResult PlaceOrder(CheckoutViewModel model)
    {
        var cart = cartController.GetCart();

        if (!ModelState.IsValid)
        {
            model.Cart = cart;
            model.Customer = customerRepository.GetCurrentCustomer(User)!;
            return View("Index", model);
        }
        
        var userId = customerRepository.GetUserId(User);
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var order = new Order
        {
            UserId = userId,
            CompanyName = model.CompanyName,
            Address = model.Address,
            City = model.City,
            PostalCode = model.PostalCode,
            Country = model.Country,
            CreditCardNumber = model.CreditCardNumber,
            ShippingProvider = model.ShippingProvider,
            Products = cart.Products,
            OrderDate = DateTime.UtcNow,
            Status = Models.Order.OrderStatus.Pending,
            TrackingNumber = GenerateTrackingNumber()
        };

        orderRepository.CreateOrder(order);

        CartController.RemoveCart(HttpContext);
        return RedirectToAction("Orders");
    }

    public IActionResult Orders()
    {
        return View(orderRepository.GetOrders());
    }

    [HttpGet]
    public IActionResult TracePackage(string? shippingProvider = "", string? trackingNumber = "")
    {
        return View(new TracePackageViewModel { ShippingProvider = shippingProvider + ".com", TrackingNumber = trackingNumber });
    }
    
    [HttpPost]
    public IActionResult TracePackage(TracePackageViewModel model)
    {
        return Redirect($"https://{model.ShippingProvider}?code={model.TrackingNumber}");
    }

    private static string GenerateTrackingNumber()
    {
        return Guid.NewGuid().ToString();
    }
}