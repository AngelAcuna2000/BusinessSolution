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

        return View(inquiries);
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
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
