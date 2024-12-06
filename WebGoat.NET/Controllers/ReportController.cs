using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.ViewModels;

namespace WebGoat.NET.Controllers;

public class ReportController : Controller
{
    public IActionResult Index()
    {
        return View(new GenerateReportViewModel { ReportName = "My Report", ReportType = "AnnualReport" });
    }

    public IActionResult Generate(GenerateReportViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var reportType = model.ReportType;
        var reportMethod = typeof(ReportGenerator).GetMethod(reportType);
        var result = reportMethod!.Invoke(new ReportGenerator(), null) as string ?? "Report generation failed";
        var bytes = System.Text.Encoding.UTF8.GetBytes(result); 
        
        return File(bytes, "application/octet-stream", $"{model.ReportName}.txt");
    }
}

public class ReportGenerator
{
    public string AnnualReport()
    {
        // Get all orders from the current year and generate a report
        return "Annual report is not implemented yet";
        
    }
    
    public string MonthlyReport()
    {
        // Generate monthly report
        return "Monthly report is not implemented yet";
    }
    
    public string ClearDatabase()
    {
        // Should never be called in production
        return "Database cleared";
    }
}