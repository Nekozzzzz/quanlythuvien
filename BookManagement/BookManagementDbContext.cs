using BookManagement._3.Models;
using BookManagement.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement
{
    public class BookManagementDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookImport> BookImports { get; set; }
        public DbSet<BookAuditLog> BookAuditLogs { get; set; }

        public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options)
        : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new ReaderConfiguration());
            modelBuilder.ApplyConfiguration(new BorrowingConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
