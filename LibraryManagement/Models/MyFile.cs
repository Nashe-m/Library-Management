using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class MyFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        [Required]
        public IFormFile Upload { get; set; }
    }
}
