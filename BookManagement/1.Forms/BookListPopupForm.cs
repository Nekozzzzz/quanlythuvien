using BookManagement._2.Controls;
using BookManagement._3.Models;
using BookManagement._4.Helpers;
using BookManagement.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManagement._1.Forms
{
    public partial class BookListPopupForm : Form
    {
        private JsonRepository<Book> _bookRepo = new JsonRepository<Book>(AppPaths.BooksFile);
        private IRepository<Book> _bookRepository = new Repository<Book>(Program.BookManagementDb);

        public Book SelectedBook { get; set; }

        public BookListPopupForm()
        {
            InitializeComponent();
            LoadBooksToGrid(_bookRepository.AsQueryable().ToList());
        }

        private void LoadBooksToGrid(List<Book> books)
        {
            listBook.DataSource = books;

            listBook.Columns["Id"].HeaderText = "Mã Sách";
            listBook.Columns["Title"].HeaderText = "Tên Sách";
            listBook.Columns["Author"].HeaderText = "Tác Giả";
            listBook.Columns["Publisher"].HeaderText = "NXB";
            listBook.Columns["Year"].HeaderText = "Năm";
            listBook.Columns["Quantity"].HeaderText = "Số Lượng";
        }

        private void listBook_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var selectedBook = (Book)listBook.CurrentRow.DataBoundItem;
            SelectedBook = selectedBook;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

}
