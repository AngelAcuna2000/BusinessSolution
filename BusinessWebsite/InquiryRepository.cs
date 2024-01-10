using Dapper;
using Shared.Models;
using System.Data;

namespace BusinessWebsite
{
    public class InquiryRepository
    {
        private readonly IDbConnection _conn;

        public InquiryRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        // Insert a new inquiry into the database
        public void InsertInquiry(Inquiry inquiryToInsert)
        {
            _conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@name, @phone, @email);",
             new { name = inquiryToInsert.Name, phone = inquiryToInsert.Phone, email = inquiryToInsert.Email, id = inquiryToInsert.Inquiry_ID });
        }
    }
}
