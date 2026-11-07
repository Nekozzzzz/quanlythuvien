using BookManagement._3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement._5.Services
{
    public class ImportService
    {
        private readonly BookManagementDbContext _db;
        public ImportService(BookManagementDbContext db) => _db = db;

        public List<BookImport> GetAll() => _db.BookImports.ToList();

        public void Import(BookImport import, string performedBy)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == import.BookId)
                ?? throw new InvalidOperationException("Khong tim thay sach.");
            _db.BookImports.Add(import);
            book.Quantity += import.Quantity;
            _db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = import.BookId, ActionType = "Nhap kho",
                ChangeDescription = $"Nhap {import.Quantity} cuon tu '{import.Supplier}' ({import.ImportPrice:N0}d) - Ton kho: {book.Quantity}",
                ChangedBy = performedBy, ChangedAt = DateTime.Now
            });
            _db.SaveChanges();
        }

        public void Update(BookImport updated, string performedBy)
        {
            var existing = _db.BookImports.FirstOrDefault(i => i.Id == updated.Id)
                ?? throw new InvalidOperationException("Khong tim thay phieu nhap.");

            if (existing.BookId == updated.BookId)
            {
                var book = _db.Books.FirstOrDefault(b => b.Id == updated.BookId);
                if (book != null)
                {
                    book.Quantity += (updated.Quantity - existing.Quantity);
                }
            }
            else
            {
                var oldBook = _db.Books.FirstOrDefault(b => b.Id == existing.BookId);
                if (oldBook != null) oldBook.Quantity -= existing.Quantity;

                var newBook = _db.Books.FirstOrDefault(b => b.Id == updated.BookId);
                if (newBook != null) newBook.Quantity += updated.Quantity;
            }

            existing.BookId = updated.BookId;
            existing.Quantity = updated.Quantity;
            existing.ImportPrice = updated.ImportPrice;
            existing.Supplier = updated.Supplier;
            existing.Notes = updated.Notes;

            _db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = updated.BookId,
                ActionType = "Sua phieu nhap",
                ChangeDescription = $"Sua phieu nhap #{updated.Id}: {updated.Quantity} cuon",
                ChangedBy = performedBy,
                ChangedAt = DateTime.Now
            });
            _db.SaveChanges();
        }

        public void Delete(int importId, string performedBy)
        {
            var existing = _db.BookImports.FirstOrDefault(i => i.Id == importId)
                ?? throw new InvalidOperationException("Khong tim thay phieu nhap.");

            var book = _db.Books.FirstOrDefault(b => b.Id == existing.BookId);
            if (book != null)
            {
                book.Quantity = Math.Max(0, book.Quantity - existing.Quantity);
            }

            _db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = existing.BookId,
                ActionType = "Xoa phieu nhap",
                ChangeDescription = $"Xoa phieu nhap #{existing.Id} ({existing.Quantity} cuon)",
                ChangedBy = performedBy,
                ChangedAt = DateTime.Now
            });

            _db.BookImports.Remove(existing);
            _db.SaveChanges();
        }
    }
}
