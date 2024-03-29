using LARemodeling.Models;
using System.Data;

namespace LARemodeling;

public class LARemodelingRepo(
    IDapperWrapper dapperWrapper,
    ILogger<LARemodelingRepo> logger,
    IDbConnection conn) : ILARemodelingRepo
{
    private readonly IDapperWrapper _dapperWrapper = dapperWrapper;
    private readonly ILogger<LARemodelingRepo> _logger = logger;
    private readonly IDbConnection _conn = conn;

    public bool InsertInquiry(InquiryModel inquiry)
    {
        try
        {
            _dapperWrapper.Execute(
                _conn, "INSERT INTO inquiries (name, phone, email) VALUES (@Name, @Phone, @Email);", inquiry);

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
            return _dapperWrapper.Query<InquiryModel>(_conn, "SELECT * FROM inquiries;");
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
            _dapperWrapper.Execute(
                _conn, "DELETE FROM inquiries WHERE inquiry_id = @Inquiry_ID;", new { inquiry.Inquiry_ID });

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting an inquiry with ID {id}", inquiry.Inquiry_ID);

            return false;
        }
    }
}
