// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebGoat.NET.Models;

namespace WebGoat.NET.Data;

public class CustomerRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
{
    public string? GetUserId(ClaimsPrincipal user)
    {
        return userManager.GetUserId(user);
    }
    
    public Customer? GetCurrentCustomer(ClaimsPrincipal? user)
    {
        if (user == null)
        {
            return null;
        }
        
        var userId = GetUserId(user);
        return userId == null ? null : context.Customers.FirstOrDefault(x => x.Id == userId);
    }
    
    public void UpdateCustomer(Customer customer)
    {
        context.Customers.Update(customer);
        context.SaveChanges();
    }
    
    public void CreateUser(IdentityUser identityUser, string email, string firstName, string lastName, string companyName, string address, string city, string country, string postalCode)
    {
        var user = new Customer
        {
            Id = identityUser.Id,
            Email = email,
            CompanyName = companyName,
            Address = address,
            City = city,
            Country = country,
            PostalCode = postalCode,
            FirstName = firstName,
            LastName = lastName
        };
        
        context.Customers.Add(user);
        context.SaveChanges();
    }
    
}