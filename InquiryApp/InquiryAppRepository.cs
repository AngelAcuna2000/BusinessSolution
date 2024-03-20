using BusinessSolutionShared;
using System.Data;

namespace InquiryApp;

public class InquiryAppRepository(
    IDapperWrapper dapperWrapper,
    ILogger<InquiryAppRepository> logger,
    IDbConnection conn) : IInquiryAppRepository
{
    private readonly IDapperWrapper _dapperWrapper = dapperWrapper;

    private readonly ILogger<InquiryAppRepository> _logger = logger;

    private readonly IDbConnection _conn = conn;

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

    public InquiryModel? GetInquiry(int id)
    {
        try
        {
            return _dapperWrapper.QuerySingle<InquiryModel>(_conn, "SELECT * FROM inquiries WHERE inquiry_id = @id", new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving an inquiry with ID {id}", id);

            return null;
        }
    }

    public bool UpdateInquiry(InquiryModel inquiry)
    {
        try
        {
            _dapperWrapper.Execute(_conn, "UPDATE inquiries SET "
                + "name = @Name, "
                + "phone = @Phone, "
                + "email = @Email WHERE "
                + "inquiry_id = @Inquiry_ID", inquiry);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating an inquiry with ID {id}", inquiry.Inquiry_ID);

            return false;
        }
    }

    public bool DeleteInquiry(InquiryModel inquiry)
    {
        try
        {
            _dapperWrapper.Execute(_conn, "DELETE FROM inquiries WHERE inquiry_id = @Inquiry_ID;", new { inquiry.Inquiry_ID });

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting an inquiry with ID {id}", inquiry.Inquiry_ID);

            return false;
        }
    }
}
