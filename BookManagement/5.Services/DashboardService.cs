using System;
using System.Linq;

namespace BookManagement._5.Services
{
    public class DashboardService
    {
        private readonly BookManagementDbContext _db;
        public DashboardService(BookManagementDbContext db) => _db = db;

        public DashboardStats GetStats() => new DashboardStats
        {
            TotalImportCost = _db.BookImports.Any() ? _db.BookImports.Sum(i => i.Quantity * i.ImportPrice) : 0,
            TotalBorrows = _db.Borrowings.Count(),
            TotalBooks   = _db.Books.Count(),
            TotalInStock = _db.Books.Any() ? _db.Books.Sum(b => b.Quantity) : 0,
            TotalReaders = _db.Readers.Count()
        };
    }

    public class DashboardStats
    {
        public decimal TotalImportCost { get; set; }
        public int TotalBorrows  { get; set; }
        public int TotalBooks    { get; set; }
        public int TotalInStock  { get; set; }
        public int TotalReaders  { get; set; }
    }
}
