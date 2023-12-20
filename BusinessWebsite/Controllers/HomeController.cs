using BusinessWebsite.Models;
using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // create a new Inquiry object with some default values
            Inquiry model = new Inquiry()
            {
                Inquiry_ID = 0,
                Date = DateTime.Now,
                Name = "",
                Phone = "",
                Email = ""
            };

            // pass the model and the message to the view using ViewBag
            // use TempData.Peek to read the message without deleting it
            ViewBag.Message = TempData.Peek("Message");
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
