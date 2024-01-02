using BusinessWebsite.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace BusinessLogic
{
    public class InquiryRepository
    {
        private readonly IDbConnection _conn;

        public InquiryRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void InsertInquiry(Inquiry inquiryToInsert)
        {
            _conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@name, @phone, @email);",
             new { name = inquiryToInsert.Name, phone = inquiryToInsert.Phone, email = inquiryToInsert.Email, id = inquiryToInsert.Inquiry_ID });
        }

        public IEnumerable<Inquiry> GetAllInquiries()
        {
            return _conn.Query<Inquiry>("SELECT * FROM inquiries;");
        }

        public Inquiry GetInquiry(int id)
        {
            return _conn.QuerySingle<Inquiry>("SELECT * FROM inquiries WHERE inquiry_id = @id", new { id = id });
        }

        public void UpdateInquiry(Inquiry inquiry)
        {
            _conn.Execute("UPDATE inquiries SET name = @name, phone = @phone, email = @email WHERE inquiry_id = @id",
             new { name = inquiry.Name, phone = inquiry.Phone, email = inquiry.Email, id = inquiry.Inquiry_ID });
        }

        public void DeleteInquiry(Inquiry inquiry)
        {
            _conn.Execute("DELETE FROM inquiries WHERE inquiry_id = @id;", new { id = inquiry.Inquiry_ID });
        }
    }
}
