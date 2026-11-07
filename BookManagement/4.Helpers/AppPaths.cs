using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement._4.Helpers
{
    public class AppPaths
    {
        public static readonly string DataFolder = @"D:\BookManagement";

        public static readonly string BooksFile = Path.Combine(DataFolder, "books.json");
        public static readonly string ReadersFile = Path.Combine(DataFolder, "readers.json");
        public static readonly string BorrowingsFile = Path.Combine(DataFolder, "borrowings.json");

        public static void EnsureDataFolder()
        {
            if (!Directory.Exists(DataFolder))
            {
                Directory.CreateDirectory(DataFolder);
            }
        }
    }
}
