﻿using InquiryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InquiryApp.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => 
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
