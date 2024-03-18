using BusinessSolution;
using BusinessWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusinessWebsite.Controllers;

public class HomeController : Controller
{
    private readonly IBusinessWebsiteRepository _repo;

    public HomeController(IBusinessWebsiteRepository repo, ILogger<HomeController> logger)
    {
        _repo = repo;
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

        if (_repo.InsertInquiry(inquiryToInsert))
        {
            TempData["Message"] = "Your inquiry has been sent.";

            return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
        }

        return RedirectToAction("Error");
    }

    public IActionResult Portfolio() => View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
