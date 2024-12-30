// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebGoat.NET.Controllers;

[AllowAnonymous]
[Route("/[controller]")]
public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SendSuggestion([FromForm] string email, string suggestion)
    {
        // Using SMTP to send email, receive the suggestion and send a mail to the user that their suggestion has been recevied
        // The suggestion should also be included in the mail to the user

        var smtpClient = new SmtpClient("127.0.0.1")
        {
            Port = 587,
            Credentials = new NetworkCredential("username", "password"),
            EnableSsl = true,
        };

        var htmlContent =
            $"<h1>Thank you for your suggestion!</h1><p>We have received your suggestion: {suggestion}</p>";
        var mailMessage = new MailMessage
        {
            From = new MailAddress("WebGoat.NET Administrator <admin@webgoat.net>"),
            Subject = "Thank you for your suggestion!",
            Body = htmlContent,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        try
        {
            smtpClient.Send(mailMessage);
        }
        catch (Exception)
        {
            // ignored
        }

        TempData["SuccessMessage"] = "Your suggestion has been sent successfully!";
        return RedirectToAction("Index");
    }
}