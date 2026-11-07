using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement._3.Models
{
    public class Borrowing : IEntity
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public int ReaderID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int QualityBrrow { get; set; }
        // Navigation properties
        public Book Book { get; set; }
        public Reader Reader { get; set; }
    }
}
