using BookManagement._3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement._5.Services
{
    public class ReportPeriod
    {
        public string Mode { get; set; } = "Tất cả thời gian";
        public DateTime? SpecificDate { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }

    public class ReportSummary
    {
        public int TotalBorrowQuantity { get; set; }
        public int TotalReturned { get; set; }
        public int TotalOverdue { get; set; }
        public decimal TotalImportCost { get; set; }
        public List<TopBookItem> TopBooks { get; set; } = new();
        public List<BorrowingDetailItem> Details { get; set; } = new();
    }

    public class TopBookItem
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; } = "";
        public string TacGia { get; set; } = "";
        public int SoLuotMuon { get; set; }
    }

    public class BorrowingDetailItem
    {
        public int Id { get; set; }
        public string TenSach { get; set; } = "";
        public string DocGia { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; } = "";
    }

    /// <summary>
    /// Service xử lý báo cáo thống kê.
    /// </summary>
    public class ReportService
    {
        private readonly BookManagementDbContext _db;

        public ReportService(BookManagementDbContext db)
        {
            _db = db;
        }

        public ReportSummary GetReport(ReportPeriod period)
        {
            var borrowings = _db.Borrowings.ToList();
            var imports = _db.BookImports.ToList();
            var books = _db.Books.ToList();
            var readers = _db.Readers.ToList();

            IEnumerable<Borrowing> filteredBorrows = borrowings;
            IEnumerable<BookImport> filteredImports = imports;

            switch (period.Mode)
            {
                case "Hôm nay":
                    var today = DateTime.Today;
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Date == today);
                    filteredImports = imports.Where(i => i.ImportDate.Date == today);
                    break;
                case "Theo Ngày cụ thể" when period.SpecificDate.HasValue:
                    var selectedDate = period.SpecificDate.Value.Date;
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Date == selectedDate);
                    filteredImports = imports.Where(i => i.ImportDate.Date == selectedDate);
                    break;
                case "Theo Tháng" when period.Month.HasValue && period.Year.HasValue:
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Month == period.Month && b.BorrowDate.Year == period.Year);
                    filteredImports = imports.Where(i => i.ImportDate.Month == period.Month && i.ImportDate.Year == period.Year);
                    break;
                case "Theo Năm" when period.Year.HasValue:
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Year == period.Year);
                    filteredImports = imports.Where(i => i.ImportDate.Year == period.Year);
                    break;
            }

            var borrowList = filteredBorrows.ToList();
            var importList = filteredImports.ToList();

            var topBooks = borrowList
                .GroupBy(b => b.BookID)
                .Select(g => new TopBookItem
                {
                    MaSach = g.Key,
                    TenSach = books.FirstOrDefault(x => x.Id == g.Key)?.Title ?? "(Đã xóa)",
                    TacGia = books.FirstOrDefault(x => x.Id == g.Key)?.Author ?? "(Không rõ)",
                    SoLuotMuon = g.Sum(x => x.QualityBrrow)
                })
                .OrderByDescending(x => x.SoLuotMuon)
                .Take(5)
                .ToList();

            var details = borrowList.Select(b => new BorrowingDetailItem
            {
                Id = b.Id,
                TenSach = books.FirstOrDefault(x => x.Id == b.BookID)?.Title ?? "(Đã xóa)",
                DocGia = readers.FirstOrDefault(x => x.Id == b.ReaderID)?.Name ?? "(Đã xóa)",
                BorrowDate = b.BorrowDate,
                DueDate = b.DueDate,
                ReturnDate = b.ReturnDate,
                SoLuong = b.QualityBrrow,
                TrangThai = b.ReturnDate.HasValue ? "Đã trả" : "Chưa trả"
            }).ToList();

            return new ReportSummary
            {
                TotalBorrowQuantity = borrowList.Sum(b => b.QualityBrrow),
                TotalReturned = borrowList.Count(b => b.ReturnDate.HasValue),
                TotalOverdue = borrowList.Count(b => !b.ReturnDate.HasValue && b.DueDate < DateTime.Now),
                TotalImportCost = importList.Sum(i => i.Quantity * i.ImportPrice),
                TopBooks = topBooks,
                Details = details
            };
        }
    }
}
