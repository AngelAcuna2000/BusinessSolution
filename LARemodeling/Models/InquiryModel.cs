namespace LARemodeling.Models;

public class InquiryModel
{
    public int Inquiry_ID { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
