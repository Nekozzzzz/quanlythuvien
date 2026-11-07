using BookManagement._3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement._5.Services
{
    public class BorrowingService
    {
        private readonly BookManagementDbContext _db;
        public BorrowingService(BookManagementDbContext db) => _db = db;

        public List<Borrowing> GetAll() => _db.Borrowings.ToList();

        public void Borrow(int bookId, int readerId, int quantity,
                           DateTime borrowDate, DateTime dueDate, string performedBy)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == bookId)
                ?? throw new InvalidOperationException("Khong tim thay sach.");
            if (book.Quantity < quantity)
                throw new InvalidOperationException($"Ton kho khong du (con: {book.Quantity}).");
            _db.Borrowings.Add(new Borrowing
            {
                BookID = bookId, ReaderID = readerId, QualityBrrow = quantity,
                BorrowDate = borrowDate, DueDate = dueDate, ReturnDate = null
            });
            book.Quantity -= quantity;
            _db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = book.Id, ActionType = "Muon sach",
                ChangeDescription = $"Muon {quantity} cuon - Con lai: {book.Quantity}",
                ChangedBy = performedBy, ChangedAt = DateTime.Now
            });
            _db.SaveChanges();
        }

        public void Return(int borrowingId, string performedBy)
        {
            var b = _db.Borrowings.FirstOrDefault(x => x.Id == borrowingId)
                ?? throw new InvalidOperationException("Khong tim thay phieu muon.");
            if (b.ReturnDate.HasValue)
                throw new InvalidOperationException("Phieu nay da tra sach truoc do.");
            b.ReturnDate = DateTime.Now;
            var book = _db.Books.FirstOrDefault(x => x.Id == b.BookID);
            if (book != null)
            {
                book.Quantity += b.QualityBrrow;
                _db.BookAuditLogs.Add(new BookAuditLog
                {
                    BookId = book.Id, ActionType = "Tra sach",
                    ChangeDescription = $"Tra {b.QualityBrrow} cuon - Ton kho: {book.Quantity}",
                    ChangedBy = performedBy, ChangedAt = DateTime.Now
                });
            }
            _db.SaveChanges();
        }
    }
}
