using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<IList<Borrowing>> GetAllBorrowings();
        Task<Borrowing?> GetBorrowingById(int id);
        Task<Borrowing> SubmitBorrowing(Borrowing payload);
        Task<Borrowing?> ReturnBook(int id);
        Task<Borrowing?> UpdateBorrowing(int id, Borrowing payload);
        Task<Borrowing?> DeleteBorowing(int id);
    }
}
