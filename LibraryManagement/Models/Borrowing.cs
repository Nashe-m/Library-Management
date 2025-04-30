using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public class Borrowing
    {
        public int Id {  get; set; }
        public DateOnly BorrowDate { get; set; }
        public DateOnly ReturnDate { get; set; }
        public int? BookId { get; set; }
        public int? MemberId { get; set; }
    }
}
