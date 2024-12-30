// Unless explicitly stated otherwise all files in this repository are licensed
// under the Apache License Version 2.0.
// This product includes software developed at Datadog (https://www.datadoghq.com/)
// Copyright 2025-present Datadog, Inc.

using System.ComponentModel.DataAnnotations;

namespace WebGoat.NET.ViewModels;

public class GenerateReportViewModel
{
    [Required]
    [Display(Name = "Report Name")]
    public required string ReportName { get; init; }
    
    [Required]
    [Display(Name = "Report Type")]
    public required string ReportType { get; init; }
    
    public string[]? ExistingReports { get; init; }
}