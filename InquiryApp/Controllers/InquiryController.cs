using InquiryApp;
using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Diagnostics;

namespace Testing.Controllers
{
    public class InquiryController : Controller
    {
        private readonly InquiryRepository _repo;

        // Constructor to inject InquiryRepository
        public InquiryController(InquiryRepository repo)
        {
            _repo = repo;
        }

        // Error action method
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Action method to display all inquiries
        public IActionResult Index()
        {
            var inquiries = _repo.GetAllInquiries();
            return View(inquiries);
        }

        // Action method to view a specific inquiry by ID
        public IActionResult ViewInquiry(int id)
        {
            var inquiry = _repo.GetInquiry(id);
            return View(inquiry);
        }

        // Action method to update an inquiry by ID
        public IActionResult UpdateInquiry(int id)
        {
            var inq = _repo.GetInquiry(id);
            return View(inq);
        }

        // Action method to update an inquiry in the database
        [HttpPost]
        public IActionResult UpdateInquiryToDatabase(Inquiry inquiry)
        {
            _repo.UpdateInquiry(inquiry);

            return RedirectToAction("ViewInquiry", new { id = inquiry.Inquiry_ID });
        }

        // Action method to delete an inquiry from the database
        [HttpPost]
        public IActionResult DeleteInquiry(Inquiry inquiry)
        {
            _repo.DeleteInquiry(inquiry);
            return RedirectToAction("Index");
        }
    }
}
