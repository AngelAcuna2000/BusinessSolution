using BusinessSolutionShared;
using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InquiryApp.Controllers;

public class HomeController(IInquiryAppRepository repo) : Controller
{
    private readonly IInquiryAppRepository _repo = repo;

    // Display table listing all inquiries in the database
    public IActionResult Index()
    {
        var inquiries = _repo.GetAllInquiries();

        if (inquiries.Any())
        {
            return View(inquiries);
        }

        return RedirectToAction("Error");
    }

    // Display table with details of a specific inquiry
    public IActionResult ViewInquiry(int id)
    {
        var inquiry = _repo.GetInquiry(id);

        if (inquiry != null)
        {
            return View(inquiry);
        }

        return RedirectToAction("Error");
    }

    // Display form for updating an inquiry
    public IActionResult UpdateInquiry(int id)
    {
        var inquiry = _repo.GetInquiry(id);

        if (inquiry != null)
        {
            return View(inquiry);
        }

        return RedirectToAction("Error");
    }

    // Update inquiry in the database and display the updated details
    [HttpPost]
    public IActionResult UpdateInquiryToDatabase(InquiryModel inquiry)
    {
        if (_repo.UpdateInquiry(inquiry))
        {
            return RedirectToAction(nameof(ViewInquiry), new { id = inquiry.Inquiry_ID });
        }

        return RedirectToAction("Error");
    }

    // Delete selected inquiry and display the updated table with all remaining inquiries
    [HttpPost]
    public IActionResult DeleteInquiry(InquiryModel inquiry)
    {
        if (_repo.DeleteInquiry(inquiry))
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("Error");
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
