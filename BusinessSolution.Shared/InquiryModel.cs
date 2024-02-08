namespace BusinessSolution.Shared.InquiryModel
{
    public class InquiryModel
    {
        public int Inquiry_ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public InquiryModel()
        {
            Inquiry_ID = 0;
            Date = DateTime.Now;
            Name = "";
            Phone = "";
            Email = "";
        }
    }
}
