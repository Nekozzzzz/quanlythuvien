using BookManagement._3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement._5.Services
{
    public class BookService
    {
        private readonly BookManagementDbContext _db;
        public BookService(BookManagementDbContext db) => _db = db;

        public List<Book> GetAll() => _db.Books.ToList();
        public Book? GetById(int id) => _db.Books.FirstOrDefault(b => b.Id == id);

        public List<Book> Search(string keyword)
        {
            keyword = keyword.ToLower();
            return _db.Books.Where(b =>
                b.Title.ToLower().Contains(keyword) ||
                b.Author.ToLower().Contains(keyword) ||
                b.Publisher.ToLower().Contains(keyword)).ToList();
        }

        public void Add(Book book, string performedBy)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            LogAudit(book.Id, "Them sach moi", $"Them '{book.Title}' - Ton kho: {book.Quantity}", performedBy);
        }

        public void Update(int id, Book updated, string performedBy)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id)
                ?? throw new InvalidOperationException("Khong tim thay sach.");
            string old = $"'{book.Title}' (SL:{book.Quantity})";
            book.Title = updated.Title; book.Author = updated.Author;
            book.Publisher = updated.Publisher; book.Year = updated.Year;
            book.Quantity = updated.Quantity;
            LogAudit(book.Id, "Cap nhat sach", $"Tu {old} -> '{book.Title}' (SL:{book.Quantity})", performedBy);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id)
                ?? throw new InvalidOperationException("Khong tim thay sach.");
            _db.Books.Remove(book);
            _db.SaveChanges();
        }

        private void LogAudit(int bookId, string action, string desc, string by)
        {
            _db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = bookId, ActionType = action,
                ChangeDescription = desc, ChangedBy = by, ChangedAt = DateTime.Now
            });
            _db.SaveChanges();
        }
    }
}
