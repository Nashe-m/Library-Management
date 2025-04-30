using LibraryManagement.DTOs;
using LibraryManagement.Helpers;
using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
    public interface IBookRepository
    {
        Task<IList<Book>> GetAllAsync(QueryObject query);
        Task<Book?> GetBookByIdAsync(int id);
        Task <Book?> AddBookAsync(Book bookModel);
        Task<Book?> UpdateBookAsync(int id, Book bookModel);
        Task<Book?> DeleteBookAsync(int id);
    }
}
