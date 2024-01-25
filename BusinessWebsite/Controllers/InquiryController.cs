using BusinessWebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Data;
using System.Diagnostics;

namespace Testing.Controllers
{
    public class InquiryController : Controller
    {
        private readonly IDbConnection _conn;

        // Constructor to inject InquiryRepository
        public InquiryController(IDbConnection conn)
        {
            _conn = conn;
        }

        // Action method to handle form submission and insert inquiry into the database
        [HttpPost]
        public IActionResult InsertInquiryToDatabase(Inquiry inquiryToInsert)
        {
            // Check if model is valid, meaning that all the required fields are filled out and have valid values
            if (ModelState.IsValid)
            {
                // Insert inquiry data to database
                _conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@name, @phone, @email);",
                 new { name = inquiryToInsert.Name, phone = inquiryToInsert.Phone, email = inquiryToInsert.Email });

                // Use TempData to store message
                TempData["Message"] = "Your inquiry has been sent.";

                // Render Home page, scroll down the page to the form, and display message stored in TempData above the form
                return RedirectToAction("Index", "Home", fragment: "Contact-Us");
            }

            // Use TempData to store message
            TempData["Message"] = "All fields are required.";

            // Render Home page, scroll down the page to the form, and display message stored in TempData above the form
            return RedirectToAction("Index", "Home", fragment: "Contact-Us");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
