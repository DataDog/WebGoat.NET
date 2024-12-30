// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

namespace WebGoat.NET.Models;

public class OrderItem
{
    public required int Id { get; set; }
    public required Product Product { get; set; }
    public required int Quantity { get; set; }
}