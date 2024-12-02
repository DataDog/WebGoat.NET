using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.ViewModels;

namespace WebGoat.NET.Controllers;

[AllowAnonymous]
[IgnoreAntiforgeryToken]
public class ErrorController : Controller
{
    public IActionResult Index([FromQuery] int code)
    {
        var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        var model = new ErrorViewModel
        {
            RequestId = requestId,
            ShowRequestId = !string.IsNullOrEmpty(requestId),
            ErrorCode = code
        };
        return View(model);
    }
}