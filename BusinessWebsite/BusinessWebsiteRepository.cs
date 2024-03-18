using BusinessSolution;
using Dapper;
using System.Data;

namespace BusinessWebsite;

public class BusinessWebsiteRepository : IBusinessWebsiteRepository
{
    private readonly IDbConnection _conn;

    private readonly ILogger<BusinessWebsiteRepository> _logger;

    public BusinessWebsiteRepository(IDbConnection conn, ILogger<BusinessWebsiteRepository> logger)
    {
        _conn = conn;

        _logger = logger;
    }

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
}
