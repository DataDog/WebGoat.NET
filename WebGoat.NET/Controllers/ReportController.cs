using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.Data;
using WebGoat.NET.ViewModels;

namespace WebGoat.NET.Controllers;

public class ReportController(CustomerRepository customerRepository) : Controller
{
    public IActionResult Index()
    {
        // Get already existing reports
        var reports = GetAllReports();
        return View(new GenerateReportViewModel { ReportName = "My Report", ReportType = "AnnualReport", ExistingReports = reports});
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
        SaveReport(model.ReportName, result);

        return File(bytes, "text/plain", $"{model.ReportName}");
    }
    
    public IActionResult ViewReport([FromQuery] string reportName)
    {
        if (!System.IO.File.Exists(reportName))
        {
            return NotFound();
        }
        
        var report = System.IO.File.ReadAllText(reportName);
        return Content(report, "text/plain");
    }
    
    private void SaveReport(string reportName, string report)
    {
        // Save report to disk
        var userId = customerRepository.GetUserId(User);
        
        // Check if a directory with the user id exists
        var directory = $"Data/Reports/{userId}";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        var path = $"{directory}/{reportName}";
        System.IO.File.WriteAllText(path, report);
    }

    private string[] GetAllReports()
    {
        var userId = customerRepository.GetUserId(User);
        var directory = $"Data/Reports/{userId}";
        
        // Get all filenames
        try 
        {
            return Directory.GetFiles(directory);
        }
        catch (DirectoryNotFoundException)
        {
            return [];
        }
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