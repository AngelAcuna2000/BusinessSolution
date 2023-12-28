using BusinessWebsite.Models;
using InquiryApp;
using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Testing.Controllers
{
    public class InquiryController : Controller
    {
        private readonly InquiryRepository _repo;

        public InquiryController(InquiryRepository repo)
        {
            _repo = repo;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult InsertInquiryToDatabase(Inquiry inquiryToInsert)
        {
            // check if the model is valid
            if (ModelState.IsValid)
            {
                _repo.InsertInquiry(inquiryToInsert);

                // use TempData to store the message
                TempData["Message"] = "Your inquiry has been sent.";

                return RedirectToAction("Index", "Home", fragment: "inquiry-form");
            }

            // use TempData to store the message
            TempData["Message"] = "All fields are required.";

            return RedirectToAction("Index", "Home", fragment: "inquiry-form");
        }
    }
}
