using BusinessSolution.Shared.InquiryModel;
using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InquiryApp.Controllers
{
    public class InquiryController : Controller
    {
        private readonly InquiryRepository _repo;

        public InquiryController(InquiryRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index() => View(_repo.GetAllInquiries());

        public IActionResult ViewInquiry(int id) => View(_repo.GetInquiry(id));

        public IActionResult UpdateInquiry(int id) => View(_repo.GetInquiry(id));

        [HttpPost]
        public IActionResult UpdateInquiryToDatabase(InquiryModel inquiry)
        {
            _repo.UpdateInquiry(inquiry);

            return RedirectToAction(nameof(ViewInquiry), new { id = inquiry.Inquiry_ID });
        }

        [HttpPost]
        public IActionResult DeleteInquiry(InquiryModel inquiry)
        {
            _repo.DeleteInquiry(inquiry);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}