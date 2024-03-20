using BusinessSolutionShared;
using System.Data;

namespace BusinessWebsite;

public class BusinessWebsiteRepository(
    IDapperWrapper dapperWrapper,
    ILogger<BusinessWebsiteRepository> logger,
    IDbConnection conn) : IBusinessWebsiteRepository
{
    private readonly IDapperWrapper _dapperWrapper = dapperWrapper;

    private readonly ILogger<BusinessWebsiteRepository> _logger = logger;

    private readonly IDbConnection _conn = conn;

    public bool InsertInquiry(InquiryModel inquiry)
    {
        try
        {
            _dapperWrapper.Execute(_conn, "INSERT INTO inquiries (name, phone, email) VALUES (@Name, @Phone, @Email);", inquiry);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while submitting an inquiry.");

            return false;
        }
    }
}
