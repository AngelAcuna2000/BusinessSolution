using BusinessWebsite.Models;
using InquiryApp;
using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Testing.Controllers
{
    public class InquiryController : Controller
    {
        private readonly InquiryRepository repo;

        public InquiryController(InquiryRepository repo)
        {
            this.repo = repo;
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
                // insert the model to the database
                repo.InsertInquiry(inquiryToInsert);

                // redirect to the Home Index action with a confirmation message and a fragment identifier
                return RedirectToAction("Index", "Home", new { message = "Your inquiry has been sent." }, fragment: "inquiry-form");
            }

            // if the model is not valid, return the same view with the model
            return RedirectToAction("Index", "Home", new { message = "All fields are required." }, fragment: "inquiry-form");
        }
    }
}
