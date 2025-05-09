using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LibraryManagement.Repository
{
   public class BorrowingRepository(DataContext context) : IBorrowingRepository
    {
        public async Task<Borrowing?> DeleteBorowing(int id)
        {
           var entity = await context.Borrowings.FindAsync(id);
            if (entity == null) return null;
            context.Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<Borrowing>> GetAllBorrowings()
        {
            var entities= await context.Borrowings.ToListAsync();
            return entities;
        }

        public async Task<Borrowing?> GetBorrowingById(int id)
        {
           var entity = await context.Borrowings.FindAsync(id);
            if (entity is null) return null;
            return entity;
        }

        public async Task<Borrowing> SubmitBorrowing(Borrowing payload)
        {
            if (payload is null) return null;
            var entity = await context.Borrowings.AddAsync(payload);
            await context.SaveChangesAsync();
            return payload;
        }

        public async Task<Borrowing?> UpdateBorrowing(int id, Borrowing payload)
        {
            var entity = await context.Borrowings.FindAsync(id);
            if (entity is null) return null;
            entity.BorrowDate = payload.BorrowDate;
            entity.ReturnDate = payload.ReturnDate;
            entity.BookId = payload.BookId;
            entity.MemberId = payload.MemberId;
            await context.SaveChangesAsync();
            return entity;

        }
        public async Task<Borrowing?> ReturnBook(int id)
        {
            var borrowing = await context.Borrowings.FindAsync(id);
            if (borrowing is null) return null;
            borrowing.isReturned = true;
            await context.SaveChangesAsync();
            return borrowing;
        }
    }
}
