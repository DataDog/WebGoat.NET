// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

namespace WebGoat.NET.Models;

public class BlogPost
{
    public required int Id { get; init; }
    
    public required string Title { get; init; }
    
    public required string Content { get; init; }
    
    public required DateTime CreatedAt { get; init; }

    public required string AuthorName { get; init; }
}