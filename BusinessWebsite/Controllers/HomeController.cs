using BusinessWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Diagnostics;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        // Display the home page with a form for inquiries
        public IActionResult Index()
        {
            // Create a new Inquiry object
            Inquiry model = new Inquiry();

            // Display the message stored in TempData
            ViewBag.Message = TempData["Message"];

            // Render the home page with a form that has the values of the Inquiry object
            // (so the form displayed will be empty)
            return View(model);
        }

        // Handles form submission: inserts inquiry into the database and redirects with appropriate message
        [HttpPost]
        public IActionResult FormSubmit(Inquiry inquiryToInsert)
        {
            // Instantiate DataHelper directly
            var dataHelper = new DataHelper();

            if (ModelState.IsValid)
            {
                // Insert the inquiry
                dataHelper.InsertInquiryToDatabase(inquiryToInsert);

                // Set success message and redirect to the Contact-Us section
                TempData["Message"] = "Your inquiry has been sent.";
                return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
            }

            // Set error message and redirect to the Contact-Us section
            TempData["Message"] = "All fields are required.";
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
