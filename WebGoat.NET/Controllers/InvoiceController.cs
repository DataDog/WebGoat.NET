// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.Models;

namespace WebGoat.NET.Controllers;

public class InvoiceController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    /*[HttpGet]
    public IActionResult Download(int orderId)
    { 
        var order = GetOrder(orderId);
        var user = GetUser(order.UserId);
        GenerateInvoice(order, user);
        var path = $"Data/Invoices/Invoice_{order.Id}_{user.CompanyName}.txt";
        return PhysicalFile(path, "text/plain", $"Invoice_{order.Id}_{user.CompanyName}.txt");
    }*/

    private void GenerateInvoice(Order order, Customer customer)
    {
        var txt = GetInvoiceTxt(order, customer);
        var path = $"Data/Invoices/Invoice_{order.Id}_{customer.CompanyName}.txt";
        System.IO.File.WriteAllText(path, txt);
    }

    private string GetInvoiceTxt(Order order, Customer customer)
    {
        var invoice = new StringBuilder();
        invoice.AppendLine("***********************************");
        invoice.AppendLine("*                                 *");
        invoice.AppendLine("*       INVOICE WEBGOAT.NET       *");
        invoice.AppendLine("*                                 *");
        invoice.AppendLine("***********************************");
        invoice.AppendLine("");

        // Invoice Details
        invoice.AppendLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd"));
        invoice.AppendLine("Invoice #: " + order.Id);
        invoice.AppendLine("Customer: " + customer.FirstName + " " + customer.LastName + " (" + customer.CompanyName + ")");
        invoice.AppendLine("==================================");

        // Items
        invoice.AppendLine("Item\t\tQty\tPrice\tTotal");
        invoice.AppendLine("----------------------------------");
        foreach (var orderedProduct in order.Products)
        {
            var product = orderedProduct.Product;
            var quantity = orderedProduct.Quantity;
            invoice.AppendLine($"{product.Name}\t{quantity}\t${product.Price}\t${product.Price * quantity}");
        }
        
        invoice.AppendLine("----------------------------------");

        // Total
        invoice.AppendLine("Total:\t\t\t\t$" + order.TotalPrice);
        invoice.AppendLine("==================================");
        invoice.AppendLine("");

        invoice.AppendLine("***********************************");
        invoice.AppendLine("*         THANK YOU FOR YOUR      *");
        invoice.AppendLine("*             BUSINESS!           *");
        invoice.AppendLine("***********************************");

        return invoice.ToString();
    }
}