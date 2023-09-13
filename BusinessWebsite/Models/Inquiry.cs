namespace InquiryApp.Models
{
    public class Inquiry
    {
        public Inquiry()
        {
        }

        public int Inquiry_ID { get; set; }  
        public DateTime Date { get ; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
