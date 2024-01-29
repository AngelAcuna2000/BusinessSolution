using BusinessWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Shared.Models;
using System.Diagnostics;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        // Redirect to the Contact-Us section on the Index page with a specified message
        private IActionResult ScrollToContactUs(string message)
        {
            TempData["Message"] = message;
            return LocalRedirect(Url.Action("Index", "Home") + "#Contact-Us");
        }

        // Get the connection string from the appsettings.json file
        private string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration.GetConnectionString("client_inquiries")!;
        }

        // Insert an inquiry into the database
        private void InsertInquiry(Inquiry inquiryToInsert)
        {
            var connectionString = GetConnectionString();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("INSERT INTO inquiries (name, phone, email) VALUES (@name, @phone, @email);", conn))
                {
                    cmd.Parameters.AddWithValue("@name", inquiryToInsert.Name);
                    cmd.Parameters.AddWithValue("@phone", inquiryToInsert.Phone);
                    cmd.Parameters.AddWithValue("@email", inquiryToInsert.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpPost]
        public IActionResult InsertInquiryToDatabase(Inquiry inquiryToInsert)
        {
            if (ModelState.IsValid)
            {
                // Insert the inquiry and redirect to the Contact-Us section with a success message
                InsertInquiry(inquiryToInsert);
                return ScrollToContactUs("Your inquiry has been sent.");
            }

            // Redirect to the Contact-Us section with an error message
            return ScrollToContactUs("All fields are required.");
        }

        // Display the home page with a form for inquiries
        public IActionResult Index()
        {
            // Create a new Inquiry object
            Inquiry model = new Inquiry();

            // Display the message stored in TempData
            ViewBag.Message = TempData["Message"];

            // Render the home page with a form that has the values of the Inquiry object (so the form displayed will be empty)
            return View(model);
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
