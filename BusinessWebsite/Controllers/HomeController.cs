using BusinessWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Create a new Inquiry object with some default values 
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

        public IActionResult Portfolio()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
