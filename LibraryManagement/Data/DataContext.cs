using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext (options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member>Members { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
