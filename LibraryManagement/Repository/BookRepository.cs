using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Helpers;

namespace LibraryManagement.Repository
{
    public class BookRepository(DataContext context) : IBookRepository
    {
      
        public async Task<Book?> AddBookAsync(Book payload)
        {
            var namecheck = await context.Books.FirstOrDefaultAsync(b=>b.Title == payload.Title);
            if (namecheck != null) return null;
            var entity = await context.Books.AddAsync(payload);
            await context.SaveChangesAsync();
            return payload;
        }

        public async Task<Book?> DeleteBookAsync(int id)
        {
            var exbook = await context.Books.FindAsync(id);
            if (exbook == null) return null;
            context.Books.Remove(exbook);
            await context.SaveChangesAsync();
            return exbook;
        }

        public async Task<IList<Book>> GetAllAsync(QueryObject query)
        {
            var books = context.Books.Include(b=>b.Borrowing).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title)) books = books.Where(b => b.Title.Contains(query.Title));
            if (!string.IsNullOrWhiteSpace(query.Author)) books =books.Where(b=>b.Author.Contains(query.Author));
            if (!string.IsNullOrWhiteSpace(query.Genre)) books = books.Where(b=>b.Genre.Contains(query.Genre)); 
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                //if (query.SortBy.Equals("Title",StringComparison.OrdinalIgnoreCase)) books = query.isDescending? books.OrderByDescending(b=>b.Title) : books.OrderBy(b=>b.Title);
                switch (query.SortBy.ToLower())
                {
                    case "title":
                        books = query.isDescending
                            ? books.OrderByDescending(b => b.Title)
                            : books.OrderBy(b => b.Title);
                        break;
                    case "author":
                        books = query.isDescending
                            ? books.OrderByDescending(b => b.Author)
                            : books.OrderBy(b => b.Author);
                        break;
                    case "published":
                        books = query.isDescending
                            ? books.OrderByDescending(b => b.Published)
                            : books.OrderBy(b => b.Published);
                        break;
                }
            }
            var skipNumber =(query.PageNumber -1) * query.PageSize;

           return await books.Skip(skipNumber).Take(query.PageSize).ToListAsync();
           
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
           var book = await context.Books.Include(b=>b.Borrowing).FirstOrDefaultAsync(b=>b.Id==id);
            if (book == null) return null;
            return book;
        }

        public async Task<Book?> UpdateBookAsync(int id, Book bookModel)
        {
           var exbook = await context.Books.FindAsync(id);
            if (exbook == null) return null;
            exbook.Title = bookModel.Title;
            exbook.Author = bookModel.Author;
            exbook.Genre = bookModel.Genre;
            exbook.ISBN = bookModel.ISBN;
            exbook.Published = bookModel.Published;
            await context.SaveChangesAsync();
            return exbook;
        }
    }
}
