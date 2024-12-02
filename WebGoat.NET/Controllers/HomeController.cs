using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.ViewModels;

namespace WebGoat.NET.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(new HomeViewModel());
    }
}