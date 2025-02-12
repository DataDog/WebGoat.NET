// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using WebGoat.NET.Models;

namespace WebGoat.NET.ViewModels;

public class CartViewModel
{
    public required Cart Cart { get; init; }
}