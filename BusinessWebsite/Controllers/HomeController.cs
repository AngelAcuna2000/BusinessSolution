using BusinessSolutionShared;
using BusinessWebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace BusinessWebsite.Controllers;

public class HomeController : Controller
{
    private readonly IDbConnection _conn;

    private readonly ILogger<HomeController> _logger;

    public HomeController(IDbConnection conn, ILogger<HomeController> logger)
    {
        _conn = conn;

        _logger = logger;
    }

    // Display home page with a form for inquiries
    public IActionResult Index()
    {
        ViewBag.Message = TempData["Message"];

        return View(new InquiryModel());
    }

    // Handle form submission
    [HttpPost]
    public IActionResult FormSubmit(InquiryModel inquiryToInsert)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = "All fields are required.";

            return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
        }

        try
        {
            _conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@Name, @Phone, @Email);", inquiryToInsert);

            TempData["Message"] = "Your inquiry has been sent.";
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "Error occurred while submitting an inquiry.");

            // Redirect to the Error action to display the error view
            return RedirectToAction("Error");
        }

        return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
    }

    public IActionResult Portfolio() => View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
