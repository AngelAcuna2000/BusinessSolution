using BusinessWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessLogic; // This is the namespace where the InquiryRepository class is defined

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

        // Action method to handle form submission and insert inquiry into the database
        [HttpPost]
        public IActionResult InsertInquiryToDatabase(Inquiry inquiryToInsert)
        {
            // Check if model is valid, meaning that all the required fields are filled out and have valid values
            if (ModelState.IsValid)
            {
                // Insert inquiry data to database
                _repo.InsertInquiry(inquiryToInsert);

                // Use TempData to store message
                TempData["Message"] = "Your inquiry has been sent.";

                // Render Home page, scroll down the page to the form, and display message stored in TempData above the form
                return RedirectToAction("Index", "Home", fragment: "Contact-Us");
            }

            // Use TempData to store the message
            TempData["Message"] = "All fields are required.";

            // Render Home page, scroll down the page to the form, and display message stored in TempData above the form
            return RedirectToAction("Index", "Home", fragment: "Contact-Us");
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
            Inquiry inq = _repo.GetInquiry(id);
            if (inq == null)
            {
                return View("InquiryNotFound");
            }
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
