using BusinessSolution;
using Dapper;
using System.Data;

namespace InquiryApp;

public class InquiryAppRepository : IInquiryAppRepository
{
    private readonly IDbConnection _conn;

    public InquiryAppRepository(IDbConnection conn) => _conn = conn;

    public IEnumerable<InquiryModel> GetAllInquiries() => _conn.Query<InquiryModel>("SELECT * FROM inquiries;");

    public InquiryModel GetInquiry(int id) =>

        _conn.QuerySingle<InquiryModel>("SELECT * FROM inquiries WHERE inquiry_id = @id", new { id });

    public void UpdateInquiry(InquiryModel inquiry) =>

        _conn.Execute("UPDATE inquiries SET "
                      + "name = @name, "
                      + "phone = @phone, "
                      + "email = @email WHERE inquiry_id = @id",
                       new
                       {
                           name = inquiry.Name,
                           phone = inquiry.Phone,
                           email = inquiry.Email,
                           id = inquiry.Inquiry_ID
                       });

    public void DeleteInquiry(InquiryModel inquiry) =>

        _conn.Execute("DELETE FROM inquiries WHERE inquiry_id = @id;",
                      new { id = inquiry.Inquiry_ID });
}
