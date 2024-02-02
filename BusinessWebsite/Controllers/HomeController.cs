using BusinessSolution.Shared.InquiryModel;
using BusinessWebsite.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbConnection _conn;

        public HomeController(IDbConnection conn)
        {
            _conn = conn;
        }

        // Display home page with a form for inquiries
        public IActionResult Index()
        {
            // Display message stored in TempData 
            ViewBag.Message = TempData["Message"];

            return View(new InquiryModel());
        }

        // Handle form submission
        [HttpPost]
        public IActionResult FormSubmit(InquiryModel inquiryToInsert)
        {
            if (ModelState.IsValid)
            {
                _conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@Name, @Phone, @Email);", inquiryToInsert);

                TempData["Message"] = "Your inquiry has been sent.";
            }
            else
            {
                TempData["Message"] = "All fields are required.";
            }

            return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
        }

        public IActionResult Portfolio() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}