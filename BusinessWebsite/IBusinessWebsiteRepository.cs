using BusinessSolutionShared;

namespace BusinessWebsite;

public interface IBusinessWebsiteRepository
{
    bool InsertInquiry(InquiryModel inquiry);
}
