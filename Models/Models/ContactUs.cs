namespace WebsitSellsLaptop.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
    }
}
