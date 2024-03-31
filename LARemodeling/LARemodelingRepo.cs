using System.Data;
using Dapper;
using LARemodeling.Models;

namespace LARemodeling;

public class LARemodelingRepo(IDbConnection conn, ILogger<LARemodelingRepo> logger) : ILARemodelingRepo
{
    private readonly ILogger<LARemodelingRepo> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IDbConnection _conn = conn ?? throw new ArgumentNullException(nameof(conn));

    public bool InsertInquiry(InquiryModel inquiry)
    {
        try
        {
            _conn.Execute("INSERT INTO inquiries (name, phone, email) VALUES (@Name, @Phone, @Email);", inquiry);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while submitting an inquiry.");

            return false;
        }
    }

    public IEnumerable<InquiryModel> GetAllInquiries()
    {
        try
        {
            return _conn.Query<InquiryModel>("SELECT * FROM inquiries;");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all inquiries.");

            return [];
        }
    }

    public bool DeleteInquiry(InquiryModel inquiry)
    {
        try
        {
            _conn.Execute("DELETE FROM inquiries WHERE inquiry_id = @Inquiry_ID;", new { inquiry.Inquiry_ID });

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting an inquiry with ID {id}", inquiry.Inquiry_ID);

            return false;
        }
    }
}
