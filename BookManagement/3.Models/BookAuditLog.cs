using System;

namespace BookManagement._3.Models
{
    public class BookAuditLog : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ActionType { get; set; } = string.Empty;
        public string? ChangeDescription { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.Now;

        public Book? Book { get; set; }
    }
}
