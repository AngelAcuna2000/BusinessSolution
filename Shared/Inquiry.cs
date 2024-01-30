namespace Shared.Models
{
    public class Inquiry
    {
        public int Inquiry_ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Inquiry()
        {
            Inquiry_ID = 0;
            Date = DateTime.Now;
            Name = "";
            Phone = "";
            Email = "";
        }
    }
}