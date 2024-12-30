// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

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