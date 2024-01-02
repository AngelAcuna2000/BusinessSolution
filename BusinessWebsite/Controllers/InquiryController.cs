using BusinessWebsite.Models;
using BusinessWebsite;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessLogic;

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

        public IActionResult Index()
        {
            var inquiries = _repo.GetAllInquiries();
            return View(inquiries);
        }

        public IActionResult ViewInquiry(int id)
        {
            var inquiry = _repo.GetInquiry(id);
            return View(inquiry);
        }

        public IActionResult UpdateInquiry(int id)
        {
            Inquiry inq = _repo.GetInquiry(id);
            if (inq == null)
            {
                return View("InquiryNotFound");
            }
            return View(inq);
        }

        [HttpPost]
        public IActionResult UpdateInquiryToDatabase(Inquiry inquiry)
        {
            _repo.UpdateInquiry(inquiry);

            return RedirectToAction("ViewInquiry", new { id = inquiry.Inquiry_ID });
        }

        [HttpPost]
        public IActionResult DeleteInquiry(Inquiry inquiry)
        {
            _repo.DeleteInquiry(inquiry);
            return RedirectToAction("Index");
        }
    }
}
