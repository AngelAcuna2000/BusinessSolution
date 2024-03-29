using LARemodeling.Models;

namespace LARemodeling;

public interface ILARemodelingRepo
{
    bool InsertInquiry(InquiryModel inquiry);
    IEnumerable<InquiryModel> GetAllInquiries();
    bool DeleteInquiry(InquiryModel inquiry);
}
