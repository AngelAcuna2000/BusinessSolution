using BusinessWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.Diagnostics;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataHelper dataHelper;

        public HomeController(DataHelper dataHelper)
        {
            this.dataHelper = dataHelper;
        }

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

        // Redirect to the Contact-Us section on the Index page with a specified message
        private IActionResult ScrollToContactUs(string message)
        {
            TempData["Message"] = message;
            return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
        }

        [HttpPost]
        public IActionResult InsertInquiryToDatabase(Inquiry inquiryToInsert)
        {
            if (ModelState.IsValid)
            {
                // Insert the inquiry and redirect to the Contact-Us section with a success message
                dataHelper.InsertInquiry(inquiryToInsert);
                return ScrollToContactUs("Your inquiry has been sent.");
            }

            // Redirect to the Contact-Us section with an error message
            return ScrollToContactUs("All fields are required.");
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
