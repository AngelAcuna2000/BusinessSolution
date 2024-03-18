﻿using BusinessSolution;
using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InquiryApp.Controllers;

public class HomeController : Controller
{
    private readonly IInquiryAppRepository _repo;

    public HomeController(IInquiryAppRepository repo) => _repo = repo;

    // Display table listing all inquiries in the database
    public IActionResult Index() => View(_repo.GetAllInquiries());

    // Display table with details of a specific inquiry
    public IActionResult ViewInquiry(int id) => View(_repo.GetInquiry(id));

    // Display form for updating an inquiry
    public IActionResult UpdateInquiry(int id) => View(_repo.GetInquiry(id));

    // Update inquiry in the database and display the updated details
    [HttpPost]
    public IActionResult UpdateInquiryToDatabase(InquiryModel inquiry)
    {
        _repo.UpdateInquiry(inquiry);

        return RedirectToAction(nameof(ViewInquiry), new { id = inquiry.Inquiry_ID });
    }

    // Delete selected inquiry and display the updated table with all remaining inquiries
    [HttpPost]
    public IActionResult DeleteInquiry(InquiryModel inquiry)
    {
        _repo.DeleteInquiry(inquiry);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}