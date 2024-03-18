using BusinessSolution;

namespace InquiryApp;

public interface IInquiryAppRepository
{
    IEnumerable<InquiryModel> GetAllInquiries();
    InquiryModel GetInquiry(int id);
    void UpdateInquiry(InquiryModel inquiry);
    void DeleteInquiry(InquiryModel inquiry);
}
