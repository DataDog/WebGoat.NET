using System.ComponentModel.DataAnnotations;

namespace WebGoat.NET.ViewModels;

public class GenerateReportViewModel
{
    [Required]
    [Display(Name = "Report Name")]
    public required string ReportName { get; set; }
    
    [Required]
    [Display(Name = "Report Type")]
    public required string ReportType { get; set; }
}