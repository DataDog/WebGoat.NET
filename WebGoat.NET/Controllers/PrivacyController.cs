using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebGoat.NET.Controllers;

[AllowAnonymous]
public class PrivacyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}