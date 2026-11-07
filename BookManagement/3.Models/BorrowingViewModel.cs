using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement._3.Models
{
    public class BorrowingViewModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string ReaderName { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int QuantityBorrow { get; set; }
    }
}
