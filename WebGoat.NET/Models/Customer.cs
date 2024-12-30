// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

namespace WebGoat.NET.Models;

public class Customer
{
    public required string Id { get; set; }
    
    public required string Email { get; set; }
    
    public required string CompanyName { get; set; }
    
    public required string Address { get; set; }
    
    public required string City { get; set; }
    
    public required string Country { get; set; }
    
    public required string PostalCode { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
}