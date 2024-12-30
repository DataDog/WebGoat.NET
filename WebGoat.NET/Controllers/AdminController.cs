// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

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