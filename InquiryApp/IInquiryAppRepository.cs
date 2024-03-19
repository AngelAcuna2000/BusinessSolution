using BusinessSolutionShared;

namespace InquiryApp;

public interface IInquiryAppRepository
{
    IEnumerable<InquiryModel> GetAllInquiries();
    InquiryModel? GetInquiry(int id);
    bool UpdateInquiry(InquiryModel inquiry);
    bool DeleteInquiry(InquiryModel inquiry);
}
