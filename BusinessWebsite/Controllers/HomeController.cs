using LARemodeling;
using LARemodeling.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusinessWebsite.Controllers;

public class HomeController(ILARemodelingRepo repo) : Controller
{
    private readonly ILARemodelingRepo _repo = repo;

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

    public IActionResult InquiryManager()
    {
        var inquiries = _repo.GetAllInquiries();

        return View(inquiries);
    }

    // Delete selected inquiry and display the updated table with all remaining inquiries
    [HttpPost]
    public IActionResult DeleteInquiry(InquiryModel inquiry)
    {
        if (_repo.DeleteInquiry(inquiry))
        {
            return RedirectToAction(nameof(InquiryManager));
        }

        return RedirectToAction("Error");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => 
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
