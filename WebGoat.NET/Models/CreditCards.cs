// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

namespace WebGoat.NET.Models;

public class CreditCards
{
    public required int Id { get; set; }
    
    public required string Number { get; set; }
    
    public required string Holder { get; set; }
    
    public required DateTime ExpirationDate { get; set; }
    
    public required string Cvv { get; set; }
}