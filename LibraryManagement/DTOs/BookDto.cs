using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs
{
    public class BookDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title is too short")]
        [MaxLength(100, ErrorMessage = "Title too Long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "Invalid Author Name")]
        [MaxLength(100, ErrorMessage = "Author too Long")]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Genre too Long")]
        public string Genre { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Invalid ISBN")]
        [MaxLength(50, ErrorMessage = "ISBN too Long")]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public DateOnly Published { get; set; }
    }
    public class AddBookDto : BookDto
    {

    }
    public class UpdateBookDto : BookDto
    {

    }
}
