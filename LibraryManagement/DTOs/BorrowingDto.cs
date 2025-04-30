using LibraryManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LibraryManagement.DTOs
{
    public class BorrowingDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateOnly BorrowDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        public DateOnly ReturnDate { get; set; }
    }
    public class AddBorrowingDto:BorrowingDto
    {

    }

    public class UpdateBorrowing:BorrowingDto
    {

    }
}
