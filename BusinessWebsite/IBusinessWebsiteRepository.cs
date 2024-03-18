using BusinessSolution;

namespace BusinessWebsite;

public interface IBusinessWebsiteRepository
{
    bool InsertInquiry(InquiryModel inquiry);
}
