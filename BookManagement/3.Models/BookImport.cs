using System;

namespace BookManagement._3.Models
{
    public class BookImport : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal ImportPrice { get; set; }
        public string? Supplier { get; set; }
        public DateTime ImportDate { get; set; } = DateTime.Now;
        public string? ImportedBy { get; set; }
        public string? Notes { get; set; }

        public Book? Book { get; set; }
    }
}
