using BusinessWebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Data;
using System.Diagnostics;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbConnection _conn;

        // Constructor to inject IDbConnection
        public HomeController(IDbConnection conn)
        {
            _conn = conn;
        }

        // Display the home page with a form for inquiries
        public IActionResult Index()
        {
            // Create a new Inquiry object with default values 
            Inquiry model = new Inquiry()
            {
                Inquiry_ID = 0,
                Date = DateTime.Now,
                Name = "",
                Phone = "",
                Email = ""
            };

            // Display the message stored in TempData
            ViewBag.Message = TempData["Message"];

            // Render the home page with a form that has the values of the Inquiry object (so the form displayed will be empty)
            return View(model);
        }

        // Handle form submission and insert inquiry into the database
        [HttpPost]
        public IActionResult InsertInquiryToDatabase(Inquiry inquiryToInsert)
        {
            // Check if model is valid, meaning that all the required fields are filled out and have valid values
            if (ModelState.IsValid)
            {
                // Insert inquiry data to the database
                _conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@name, @phone, @email);",
                    new { name = inquiryToInsert.Name, phone = inquiryToInsert.Phone, email = inquiryToInsert.Email });

                // Use TempData to store message
                TempData["Message"] = "Your inquiry has been sent.";

                // Redirect to the home page, scroll down to the form, and display the message stored in TempData above the form
                return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
            }

            // Use TempData to store an error message
            TempData["Message"] = "All fields are required.";

            // Redirect to the home page, scroll down to the form, and display the error message stored in TempData above the form
            return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
        }

        // Display the portfolio page
        public IActionResult Portfolio()
        {
            return View();
        }

        // Display the privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Handle errors and display an error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
