using Dapper;
using MySql.Data.MySqlClient;
using Shared.Models;

namespace BusinessWebsite
{
    public class DataHelper
    {
        // Get the connection string from the appsettings.json file
        private string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration.GetConnectionString("client_inquiries")!;
        }

        // Insert an inquiry into the database using Dapper
        public void InsertInquiry(Inquiry inquiryToInsert)
        {
            var connectionString = GetConnectionString();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Using Dapper to execute the query
                conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@Name, @Phone, @Email);", inquiryToInsert);
            }
        }
    }
}
