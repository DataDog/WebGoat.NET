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