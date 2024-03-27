using BusinessSolutionShared;

namespace InquiryApp;

public interface IInquiryAppRepository
{
    IEnumerable<InquiryModel> GetAllInquiries();
    bool DeleteInquiry(InquiryModel inquiry);
}
