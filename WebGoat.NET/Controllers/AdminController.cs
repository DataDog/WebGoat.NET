using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebGoat.NET.Controllers;

public class AdminController : Controller
{
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public IActionResult Index()
    {
        return View();
    }
}