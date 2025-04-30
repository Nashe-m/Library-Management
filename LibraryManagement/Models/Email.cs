namespace LibraryManagement.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Templatekey { get; set; } 
        public string ToEmail { get; set; }
        public string ccEmail { get; set; }
        public string bcEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
