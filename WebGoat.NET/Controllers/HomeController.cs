// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

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