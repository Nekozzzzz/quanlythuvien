using BookManagement._1.Forms;
using BookManagement._3.Models;
using BookManagement._4.Helpers;
using BookManagement._5.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BookManagement._1.Forms
{
    public partial class MainForm : Form
    {
        public bool IsLoggedOut { get; private set; } = false;
        public Panel? CurrentActiveView { get; private set; }

        private int selectedBorrowBookId = 0;
        private int selectedBorrowReaderId = 0;
        private Button? currentNavButton = null;

        private readonly BookService _bookService;
        private readonly ReaderService _readerService;
        private readonly BorrowingService _borrowingService;
        private readonly ImportService _importService;
        private readonly UserService _userService;
        private readonly ReportService _reportService;
        private readonly DashboardService _dashboardService;

        public MainForm()
        {
            InitializeComponent();
            var db = Program.BookManagementDb;
            _bookService = new BookService(db);
            _readerService = new ReaderService(db);
            _borrowingService = new BorrowingService(db);
            _importService = new ImportService(db);
            _userService = new UserService(db);
            _reportService = new ReportService(db);
            _dashboardService = new DashboardService(db);
            SetupEventHandlers();
            ApplyPermissions();
            LoadAllData();
            Application.AddMessageFilter(new MouseWheelMessageFilter(this));
            ShowView(viewDashboard, "Hệ thống Quản lý Bán hàng - Trang Chủ", btnNavHome);
        }



        private void SetupEventHandlers()
        {
            // Sidebar Navigation
            btnNavHome.Click += (s, e) => ShowView(viewDashboard, "Hệ thống Quản lý Bán hàng - Trang Chủ", btnNavHome);
            btnNavBorrow.Click += (s, e) => ShowView(viewBorrow, "Bán Hàng & Mượn Trả Sách (POS)", btnNavBorrow);
            btnNavBooks.Click += (s, e) => ShowView(viewBooks, "Quản Lý Sản Phẩm & Tồn Kho Thực Tế", btnNavBooks);
            btnNavReaders.Click += (s, e) => ShowView(viewReaders, "Quản Lý Khách Hàng (Độc Giả)", btnNavReaders);
            btnNavImports.Click += (s, e) => ShowView(viewImports, "Quản Lý Nhập Hàng & Lịch Sử Biến Động", btnNavImports);
            btnNavUsers.Click += (s, e) => {
                if (!SessionManager.IsAdmin)
                {
                    MessageBox.Show("Bạn không có quyền truy cập vào mục này. Chức năng chỉ dành cho Quản trị viên (Admin).", "Giới hạn quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ShowView(viewUsers, "Quản Lý Tài Khoản & Phân Quyền Hệ Thống", btnNavUsers);
            };
            btnNavReports.Click += (s, e) => ShowView(viewReports, "Báo Cáo Thống Kê Số Liệu", btnNavReports);
            btnNavProfile.Click += (s, e) => ShowView(viewProfile, "Quản Lý Tài Khoản Hiện Tại Của Mình", btnNavProfile);

            btnNavLogout.Click += (s, e) => {
                if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SessionManager.Logout();
                    IsLoggedOut = true;
                    this.Close();
                }
            };

            btnNavExit.Click += (s, e) => {
                if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IsLoggedOut = false;
                    this.Close();
                }
            };

            // Quick Access Buttons on Dashboard
            btnQuickBorrow.Click += (s, e) => ShowView(viewBorrow, "Bán Hàng & Mượn Trả Sách (POS)", btnNavBorrow);
            btnQuickBooks.Click += (s, e) => ShowView(viewBooks, "Quản Lý Sản Phẩm & Tồn Kho Thực Tế", btnNavBooks);
            btnQuickReaders.Click += (s, e) => ShowView(viewReaders, "Quản Lý Khách Hàng (Độc Giả)", btnNavReaders);
            btnQuickReports.Click += (s, e) => ShowView(viewReports, "Báo Cáo Thống Kê Số Liệu", btnNavReports);
            btnQuickImports.Click += (s, e) => ShowView(viewImports, "Quản Lý Nhập Hàng & Lịch Sử Biến Động", btnNavImports);
            btnQuickUsers.Click += (s, e) => {
                if (!SessionManager.IsAdmin)
                {
                    MessageBox.Show("Bạn không có quyền truy cập vào mục này. Chức năng chỉ dành cho Quản trị viên (Admin).", "Giới hạn quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ShowView(viewUsers, "Quản Lý Tài Khoản & Phân Quyền Hệ Thống", btnNavUsers);
            };

            // Mượn Trả Events
            btnChooseBook.Click += btnChooseBook_Click;
            btnChooseReader.Click += btnChooseReader_Click;
            btnActionBorrow.Click += btnActionBorrow_Click;
            btnActionReturn.Click += btnActionReturn_Click;
            btnRefreshBorrow.Click += (s, e) => LoadBorrowData();
            btnSearchBorrow.Click += btnSearchBorrow_Click;
            dgvBorrowings.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvBorrowings.Columns[e.ColumnIndex].Name == "HanConLai" && e.Value != null)
                {
                    string val = e.Value.ToString() ?? "";
                    if (val.Contains("Đã trả"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(33, 150, 83);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                    else if (val.Contains("Còn"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(0, 128, 128);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                    else if (val.Contains("Hạn chót"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                    else if (val.Contains("Quá hạn"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 53, 69);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                }
            };

            // Books Events
            btnBookAdd.Click += btnBookAdd_Click;
            btnBookUpdate.Click += btnBookUpdate_Click;
            btnBookDelete.Click += btnBookDelete_Click;
            btnBookClear.Click += (s, e) => ClearBookInputs();
            btnSearchBook.Click += btnSearchBook_Click;
            btnRefreshBook.Click += (s, e) => LoadBooksData();
            dgvBooks.CellClick += dgvBooks_CellClick;

            // Readers Events
            btnReaderAdd.Click += btnReaderAdd_Click;
            btnReaderUpdate.Click += btnReaderUpdate_Click;
            btnReaderDelete.Click += btnReaderDelete_Click;
            btnReaderClear.Click += (s, e) => ClearReaderInputs();
            btnSearchReader.Click += btnSearchReader_Click;
            dgvReaders.CellClick += dgvReaders_CellClick;

            // Imports Events
            btnExecuteImport.Click += btnExecuteImport_Click;
            btnImportUpdate.Click += btnImportUpdate_Click;
            btnImportDelete.Click += btnImportDelete_Click;
            btnImportClear.Click += (s, e) => ClearImportInputs();
            dgvImportHistory.CellClick += dgvImportHistory_CellClick;

            // Users Events
            btnUserAdd.Click += btnUserAdd_Click;
            btnUserUpdate.Click += btnUserUpdate_Click;
            btnUserDelete.Click += btnUserDelete_Click;
            dgvUsers.CellClick += dgvUsers_CellClick;

            // Reports Events
            cboReportPeriod.SelectedIndexChanged += cboReportPeriod_SelectedIndexChanged;
            btnFilterReport.Click += btnFilterReport_Click;

            // Profile Events
            btnProfSave.Click += btnProfSave_Click;
            btnChangePassword.Click += btnChangePassword_Click;
        }

        private void ShowView(Panel targetView, string title, Button navBtn)
        {
            lblPageHeaderTitle.Text = title;

            // Reset background for previous nav button
            if (currentNavButton != null)
            {
                currentNavButton.BackColor = Color.Transparent;
                currentNavButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }

            // Highlight current button
            currentNavButton = navBtn;
            if (currentNavButton != null)
            {
                currentNavButton.BackColor = Color.FromArgb(0, 180, 170);
                currentNavButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }

            // Hide all views and show target
            viewDashboard.Visible = (targetView == viewDashboard);
            viewBorrow.Visible = (targetView == viewBorrow);
            viewBooks.Visible = (targetView == viewBooks);
            viewReaders.Visible = (targetView == viewReaders);
            viewImports.Visible = (targetView == viewImports);
            viewUsers.Visible = (targetView == viewUsers);
            viewReports.Visible = (targetView == viewReports);
            viewProfile.Visible = (targetView == viewProfile);

            targetView.BringToFront();
            CurrentActiveView = targetView;
            targetView.Focus();

            // Refresh specific view if needed
            if (targetView == viewDashboard) LoadDashboardData();
            else if (targetView == viewReports) RunReports();
            else if (targetView == viewProfile) LoadProfileData();
        }

        private void ApplyPermissions()
        {
            var user = SessionManager.CurrentUser;
            if (user != null)
            {
                lblHeaderUserInfo.Text = $"Xin chào:     {user.FullName}\nChức vụ:     {user.Role}";
                lblFooterStatus.Text = $"🟢 Hệ thống sẵn sàng (v2.0)    |    CSDL: MySQL (bookmanagementdb)    |    Tài khoản: {user.Username} ({user.Role})";

                if (SessionManager.IsCustomer)
                {
                    // 1. Phân quyền cho Khách Hàng (Customer)
                    // Ẩn các menu quản lý nhân viên, khách hàng, nhập hàng, báo cáo
                    btnNavUsers.Visible = false;
                    btnNavReaders.Visible = false;
                    btnNavImports.Visible = false;
                    btnNavReports.Visible = false;

                    // Ẩn các nút nhanh quản trị trên Dashboard
                    btnQuickImports.Visible = false;
                    btnQuickUsers.Visible = false;
                    btnQuickReaders.Visible = false;
                    btnQuickReports.Visible = false;
                    cardDashRevenue.Visible = false;

                    // Khách chỉ được xem danh sách sách, không được thêm/sửa/xóa
                    btnBookAdd.Visible = false;
                    btnBookUpdate.Visible = false;
                    btnBookDelete.Visible = false;
                    btnBookClear.Visible = false;
                    grpBookInput.Visible = false;
                    lblInventoryAlertBanner.Visible = false;

                    // Màn hình mượn trả: Tự động gán người mượn là chính khách hàng
                    var db = Program.BookManagementDb;
                    var reader = db.Readers.FirstOrDefault(r =>
                        (!string.IsNullOrEmpty(user.Phone) && r.Phone == user.Phone) ||
                        (!string.IsNullOrEmpty(user.Email) && r.Email == user.Email) ||
                        r.Name == user.FullName);

                    if (reader == null)
                    {
                        reader = new Reader
                        {
                            Name = user.FullName,
                            Phone = user.Phone ?? "",
                            Email = user.Email ?? ""
                        };
                        db.Readers.Add(reader);
                        db.SaveChanges();
                    }

                    selectedBorrowReaderId = reader.Id;
                    txtBorrowReader.Text = reader.Name;
                    txtBorrowReader.Tag = reader.Id;
                    btnChooseReader.Visible = false;
                    txtSearchBorrow.PlaceholderText = "Tìm theo tên sách bạn đã mượn...";
                }
                else if (!SessionManager.IsAdmin)
                {
                    btnNavUsers.Enabled = false;
                    btnNavUsers.ForeColor = Color.FromArgb(100, 116, 139);
                    btnQuickUsers.Enabled = false;
                    btnQuickUsers.BackColor = Color.FromArgb(241, 245, 249);
                    btnBookDelete.Enabled = false;
                    btnReaderDelete.Enabled = false;
                }
            }
        }

        private void LoadAllData()
        {
            LoadDashboardData();
            LoadBorrowData();
            LoadBooksData();
            LoadReadersData();
            LoadImportsData();
            LoadUsersData();
            LoadProfileData();
        }

        // ==========================================
        // 1. DASHBOARD
        // ==========================================
        private void LoadDashboardData()
        {
            try
            {
                var stats = _dashboardService.GetStats();
                lblCardCostVal.Text = string.Format("{0:N0} đ", stats.TotalImportCost);
                lblCardBorrowsVal.Text = $"{stats.TotalBorrows} đơn";
                lblCardBooksVal.Text = $"{stats.TotalBooks} SP";
                lblCardBooksSub.Text = $"{stats.TotalInStock} cuốn tồn kho";
                lblCardReadersVal.Text = $"{stats.TotalReaders} khách";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Dashboard Load Error: " + ex.Message);
            }
        }

        // ==========================================
        // 2. MƯỢN TRẢ SÁCH (POS)
        // ==========================================
        private void LoadBorrowData(string keyword = "")
        {
            try
            {
                var db = Program.BookManagementDb;
                var borrowings = db.Borrowings.ToList();
                var books = db.Books.ToList();
                var readers = db.Readers.ToList();

                // Nếu là khách hàng, chỉ hiển thị danh sách mượn sách của chính khách hàng này
                if (SessionManager.IsCustomer && selectedBorrowReaderId > 0)
                {
                    borrowings = borrowings.Where(b => b.ReaderID == selectedBorrowReaderId).ToList();
                }

                var list = borrowings
                    .OrderByDescending(b => b.BorrowDate)
                    .Select(b => {
                        int daysLeft = (b.DueDate.Date - DateTime.Today).Days;
                        string hanConLai;
                        if (b.ReturnDate.HasValue)
                        {
                            hanConLai = "✔️ Đã trả";
                        }
                        else if (daysLeft > 0)
                        {
                            hanConLai = $"⏳ Còn {daysLeft} ngày";
                        }
                        else if (daysLeft == 0)
                        {
                            hanConLai = "⚠️ Hạn chót hôm nay";
                        }
                        else
                        {
                            hanConLai = $"🚨 Quá hạn {Math.Abs(daysLeft)} ngày";
                        }

                        return new
                        {
                            b.Id,
                            TenSach = books.FirstOrDefault(x => x.Id == b.BookID)?.Title ?? "(Không rõ)",
                            DocGia = readers.FirstOrDefault(x => x.Id == b.ReaderID)?.Name ?? "(Không rõ)",
                            b.BorrowDate,
                            b.DueDate,
                            b.ReturnDate,
                            SoLuong = b.QualityBrrow,
                            TrangThai = b.ReturnDate.HasValue ? "✅ Đã trả" : (b.DueDate < DateTime.Now ? "❌ Quá hạn" : "⏳ Đang mượn"),
                            HanConLai = hanConLai
                        };
                    }).ToList();

                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    list = list.Where(x => x.TenSach.ToLower().Contains(keyword) || x.DocGia.ToLower().Contains(keyword)).ToList();
                }

                dgvBorrowings.DataSource = list;

                if (dgvBorrowings.Columns.Count > 0)
                {
                    dgvBorrowings.Columns["Id"].HeaderText = "Mã Phiếu";
                    dgvBorrowings.Columns["TenSach"].HeaderText = "Tên Sách";
                    dgvBorrowings.Columns["DocGia"].HeaderText = "Tên Độc Giả";
                    dgvBorrowings.Columns["BorrowDate"].HeaderText = "Ngày Mượn";
                    dgvBorrowings.Columns["DueDate"].HeaderText = "Hạn Trả";
                    dgvBorrowings.Columns["ReturnDate"].HeaderText = "Ngày Trả";
                    dgvBorrowings.Columns["SoLuong"].HeaderText = "Số Lượng";
                    dgvBorrowings.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    if (dgvBorrowings.Columns.Contains("HanConLai"))
                    {
                        dgvBorrowings.Columns["HanConLai"].HeaderText = "Hạn Còn Lại";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu mượn trả: " + ex.Message);
            }
        }

        private void btnChooseBook_Click(object? sender, EventArgs e)
        {
            using (var popup = new BookListPopupForm())
            {
                if (popup.ShowDialog() == DialogResult.OK && popup.SelectedBook != null)
                {
                    selectedBorrowBookId = popup.SelectedBook.Id;
                    txtBorrowBook.Text = popup.SelectedBook.Title;
                }
            }
        }

        private void btnChooseReader_Click(object? sender, EventArgs e)
        {
            using (var popup = new ReaderListPopupForm())
            {
                if (popup.ShowDialog() == DialogResult.OK && popup.SelectedReader != null)
                {
                    selectedBorrowReaderId = popup.SelectedReader.Id;
                    txtBorrowReader.Text = popup.SelectedReader.Name;
                }
            }
        }

        private void btnActionBorrow_Click(object? sender, EventArgs e)
        {
            if (selectedBorrowBookId <= 0 || selectedBorrowReaderId <= 0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Sách và Độc giả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int qty = (int)numBorrowQty.Value;
                string performedBy = SessionManager.CurrentUser?.FullName ?? "Hệ thống";

                _borrowingService.Borrow(
                    selectedBorrowBookId,
                    selectedBorrowReaderId,
                    qty,
                    dtpBorrowDate.Value,
                    dtpDueDate.Value,
                    performedBy
                );

                MessageBox.Show("Lập phiếu mượn sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBorrowData();
                LoadBooksData();
                LoadDashboardData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi mượn sách", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActionReturn_Click(object? sender, EventArgs e)
        {
            if (dgvBorrowings.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu mượn trong danh sách để trả sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int borrowingId = Convert.ToInt32(dgvBorrowings.CurrentRow.Cells["Id"].Value);
            var db = Program.BookManagementDb;
            var borrowing = db.Borrowings.FirstOrDefault(b => b.Id == borrowingId);

            if (borrowing == null)
            {
                MessageBox.Show("Không tìm thấy phiếu mượn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (borrowing.ReturnDate.HasValue)
            {
                MessageBox.Show("Phiếu này đã được trả sách trước đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            borrowing.ReturnDate = DateTime.Now;

            // Hoàn lại số lượng tồn kho
            var book = db.Books.FirstOrDefault(b => b.Id == borrowing.BookID);
            if (book != null)
            {
                book.Quantity += borrowing.QualityBrrow;

                db.BookAuditLogs.Add(new BookAuditLog
                {
                    BookId = book.Id,
                    ActionType = "Trả sách",
                    ChangeDescription = $"Trả {borrowing.QualityBrrow} cuốn (Tồn kho sau trả: {book.Quantity})",
                    ChangedBy = SessionManager.CurrentUser?.FullName ?? "Hệ thống",
                    ChangedAt = DateTime.Now
                });
            }

            db.SaveChanges();
            MessageBox.Show("Xác nhận trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadBorrowData();
            LoadBooksData();
            LoadDashboardData();
        }

        private void btnSearchBorrow_Click(object? sender, EventArgs e)
        {
            LoadBorrowData(txtSearchBorrow.Text.Trim());
        }

        // ==========================================
        // 3. QUẢN LÝ SÁCH & TỒN KHO
        // ==========================================
        private void LoadBooksData(string keyword = "")
        {
            try
            {
                var db = Program.BookManagementDb;
                var books = db.Books.ToList();
                var borrowings = db.Borrowings.Where(b => b.ReturnDate == null).ToList();

                var list = books.Select(b => {
                    int borrowedCount = borrowings.Where(x => x.BookID == b.Id).Sum(x => x.QualityBrrow);
                    return new
                    {
                        b.Id,
                        b.Title,
                        b.Author,
                        b.Publisher,
                        b.Year,
                        TonKho = b.Quantity,
                        DangMuon = borrowedCount,
                        TrangThaiTon = b.Quantity <= 5 ? "⚠️ Sắp hết hàng" : "🟢 Đầy đủ"
                    };
                }).ToList();

                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    list = list.Where(x => x.Title.ToLower().Contains(keyword) || x.Author.ToLower().Contains(keyword) || x.Publisher.ToLower().Contains(keyword)).ToList();
                }

                dgvBooks.DataSource = list;

                if (dgvBooks.Columns.Count > 0)
                {
                    dgvBooks.Columns["Id"].HeaderText = "Mã Sách";
                    dgvBooks.Columns["Title"].HeaderText = "Tên Sách";
                    dgvBooks.Columns["Author"].HeaderText = "Tác Giả";
                    dgvBooks.Columns["Publisher"].HeaderText = "Nhà Xuất Bản";
                    dgvBooks.Columns["Year"].HeaderText = "Năm XB";
                    dgvBooks.Columns["TonKho"].HeaderText = "Tồn Kho Hiện Tại";
                    dgvBooks.Columns["DangMuon"].HeaderText = "Đang Cho Mượn";
                    dgvBooks.Columns["TrangThaiTon"].HeaderText = "Trạng Thái Tồn";
                }

                // Cập nhật combobox nhập hàng
                LoadImportBooksCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục sách: " + ex.Message);
            }
        }

        private void dgvBooks_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvBooks.CurrentRow == null) return;
            var row = dgvBooks.CurrentRow;

            txtBookTitle.Text = row.Cells["Title"].Value?.ToString();
            txtBookAuthor.Text = row.Cells["Author"].Value?.ToString();
            txtBookPublisher.Text = row.Cells["Publisher"].Value?.ToString();
            numBookYear.Value = Convert.ToDecimal(row.Cells["Year"].Value ?? 2024);
            numBookQuantity.Value = Convert.ToDecimal(row.Cells["TonKho"].Value ?? 0);
        }

        private void btnBookAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var db = Program.BookManagementDb;
            var book = new Book
            {
                Title = txtBookTitle.Text.Trim(),
                Author = txtBookAuthor.Text.Trim(),
                Publisher = txtBookPublisher.Text.Trim(),
                Year = (int)numBookYear.Value,
                Quantity = (int)numBookQuantity.Value
            };

            db.Books.Add(book);
            db.SaveChanges();

            // Ghi nhật ký
            db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = book.Id,
                ActionType = "Thêm sách mới",
                ChangeDescription = $"Thêm mới đầu sách '{book.Title}' - Tồn kho ban đầu: {book.Quantity}",
                ChangedBy = SessionManager.CurrentUser?.FullName ?? "Hệ thống",
                ChangedAt = DateTime.Now
            });
            db.SaveChanges();

            MessageBox.Show("Thêm sách mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearBookInputs();
            LoadBooksData();
            LoadDashboardData();
        }

        private void btnBookUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvBooks.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvBooks.CurrentRow.Cells["Id"].Value);
            var db = Program.BookManagementDb;
            var book = db.Books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                string oldInfo = $"'{book.Title}' (SL: {book.Quantity})";
                book.Title = txtBookTitle.Text.Trim();
                book.Author = txtBookAuthor.Text.Trim();
                book.Publisher = txtBookPublisher.Text.Trim();
                book.Year = (int)numBookYear.Value;
                book.Quantity = (int)numBookQuantity.Value;

                db.BookAuditLogs.Add(new BookAuditLog
                {
                    BookId = book.Id,
                    ActionType = "Cập nhật sách",
                    ChangeDescription = $"Cập nhật thông tin sách từ {oldInfo} thành '{book.Title}' (SL: {book.Quantity})",
                    ChangedBy = SessionManager.CurrentUser?.FullName ?? "Hệ thống",
                    ChangedAt = DateTime.Now
                });

                db.SaveChanges();
                MessageBox.Show("Cập nhật sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooksData();
                LoadDashboardData();
            }
        }

        private void btnBookDelete_Click(object? sender, EventArgs e)
        {
            if (dgvBooks.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvBooks.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa đầu sách này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var db = Program.BookManagementDb;
                var book = db.Books.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    MessageBox.Show("Xóa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearBookInputs();
                    LoadBooksData();
                    LoadDashboardData();
                }
            }
        }

        private void ClearBookInputs()
        {
            txtBookTitle.Clear();
            txtBookAuthor.Clear();
            txtBookPublisher.Clear();
            numBookYear.Value = 2024;
            numBookQuantity.Value = 10;
        }

        private void btnSearchBook_Click(object? sender, EventArgs e)
        {
            LoadBooksData(txtSearchBook.Text.Trim());
        }

        // ==========================================
        // 4. QUẢN LÝ ĐỘC GIẢ
        // ==========================================
        private void LoadReadersData(string keyword = "")
        {
            try
            {
                var db = Program.BookManagementDb;
                var readers = db.Readers.ToList();

                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    readers = readers.Where(r => r.Name.ToLower().Contains(keyword) || (r.Email != null && r.Email.ToLower().Contains(keyword)) || (r.Phone != null && r.Phone.Contains(keyword))).ToList();
                }

                dgvReaders.DataSource = readers;
                if (dgvReaders.Columns.Count > 0)
                {
                    dgvReaders.Columns["Id"].HeaderText = "Mã Độc Giả";
                    dgvReaders.Columns["Name"].HeaderText = "Họ và Tên";
                    dgvReaders.Columns["Email"].HeaderText = "Email";
                    dgvReaders.Columns["Phone"].HeaderText = "Số Điện Thoại";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách độc giả: " + ex.Message);
            }
        }

        private void dgvReaders_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvReaders.CurrentRow == null) return;
            var row = dgvReaders.CurrentRow;

            txtReaderName.Text = row.Cells["Name"].Value?.ToString();
            txtReaderEmail.Text = row.Cells["Email"].Value?.ToString();
            txtReaderPhone.Text = row.Cells["Phone"].Value?.ToString();
        }

        private void btnReaderAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReaderName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên độc giả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var db = Program.BookManagementDb;
            var reader = new Reader
            {
                Name = txtReaderName.Text.Trim(),
                Email = txtReaderEmail.Text.Trim(),
                Phone = txtReaderPhone.Text.Trim()
            };

            db.Readers.Add(reader);
            db.SaveChanges();
            MessageBox.Show("Thêm độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearReaderInputs();
            LoadReadersData();
            LoadDashboardData();
        }

        private void btnReaderUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvReaders.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvReaders.CurrentRow.Cells["Id"].Value);
            var db = Program.BookManagementDb;
            var reader = db.Readers.FirstOrDefault(r => r.Id == id);
            if (reader != null)
            {
                reader.Name = txtReaderName.Text.Trim();
                reader.Email = txtReaderEmail.Text.Trim();
                reader.Phone = txtReaderPhone.Text.Trim();

                db.SaveChanges();
                MessageBox.Show("Cập nhật độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReadersData();
            }
        }

        private void btnReaderDelete_Click(object? sender, EventArgs e)
        {
            if (dgvReaders.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvReaders.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa độc giả này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var db = Program.BookManagementDb;
                var reader = db.Readers.FirstOrDefault(r => r.Id == id);
                if (reader != null)
                {
                    db.Readers.Remove(reader);
                    db.SaveChanges();
                    MessageBox.Show("Xóa độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearReaderInputs();
                    LoadReadersData();
                    LoadDashboardData();
                }
            }
        }

        private void ClearReaderInputs()
        {
            txtReaderName.Clear();
            txtReaderEmail.Clear();
            txtReaderPhone.Clear();
        }

        private void btnSearchReader_Click(object? sender, EventArgs e)
        {
            LoadReadersData(txtSearchReader.Text.Trim());
        }

        // ==========================================
        // 5. QUẢN LÝ NHẬP HÀNG & LỊCH SỬ SỬA ĐỔI
        // ==========================================
        private void LoadImportBooksCombo()
        {
            try
            {
                var books = Program.BookManagementDb.Books.ToList();
                cboImportBook.DataSource = books;
                cboImportBook.DisplayMember = "Title";
                cboImportBook.ValueMember = "Id";
            }
            catch { }
        }

        private void LoadImportsData()
        {
            try
            {
                var db = Program.BookManagementDb;
                var imports = db.BookImports.ToList();
                var books = db.Books.ToList();

                var importView = imports.OrderByDescending(i => i.ImportDate).Select(i => new {
                    i.Id,
                    TenSach = books.FirstOrDefault(b => b.Id == i.BookId)?.Title ?? "(Đã xóa)",
                    i.Quantity,
                    GiaNhap = string.Format("{0:N0} đ", i.ImportPrice),
                    ThanhTien = string.Format("{0:N0} đ", i.Quantity * i.ImportPrice),
                    i.Supplier,
                    i.ImportDate,
                    i.ImportedBy,
                    i.Notes
                }).ToList();

                dgvImportHistory.DataSource = importView;
                if (dgvImportHistory.Columns.Count > 0)
                {
                    dgvImportHistory.Columns["Id"].HeaderText = "Mã Phiếu";
                    dgvImportHistory.Columns["TenSach"].HeaderText = "Tên Sách Nhập";
                    dgvImportHistory.Columns["Quantity"].HeaderText = "Số Lượng";
                    dgvImportHistory.Columns["GiaNhap"].HeaderText = "Đơn Giá";
                    dgvImportHistory.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                    dgvImportHistory.Columns["Supplier"].HeaderText = "Nhà Cung Cấp";
                    dgvImportHistory.Columns["ImportDate"].HeaderText = "Ngày Nhập";
                    dgvImportHistory.Columns["ImportedBy"].HeaderText = "Người Nhập";
                    dgvImportHistory.Columns["Notes"].HeaderText = "Ghi Chú";
                }

                // Load Audit Logs
                var allLogs = db.BookAuditLogs.OrderByDescending(l => l.ChangedAt).ToList();
                var logs = allLogs.Select(l => new {
                    l.Id,
                    TenSach = books.FirstOrDefault(b => b.Id == l.BookId) != null ? books.First(b => b.Id == l.BookId).Title : "(Đầu sách)",
                    l.ActionType,
                    l.ChangeDescription,
                    l.ChangedBy,
                    ThoiGian = l.ChangedAt
                }).ToList();

                dgvAuditLogs.DataSource = logs;
                if (dgvAuditLogs.Columns.Count > 0)
                {
                    dgvAuditLogs.Columns["Id"].HeaderText = "Mã Nhật Ký";
                    dgvAuditLogs.Columns["TenSach"].HeaderText = "Tên Sách";
                    dgvAuditLogs.Columns["ActionType"].HeaderText = "Hành Động";
                    dgvAuditLogs.Columns["ChangeDescription"].HeaderText = "Chi Tiết Biến Động";
                    dgvAuditLogs.Columns["ChangedBy"].HeaderText = "Người Thực Hiện";
                    dgvAuditLogs.Columns["ThoiGian"].HeaderText = "Thời Gian";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử nhập hàng: " + ex.Message);
            }
        }

        private void btnExecuteImport_Click(object? sender, EventArgs e)
        {
            if (cboImportBook.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầu sách cần nhập hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = Convert.ToInt32(cboImportBook.SelectedValue);
            int qty = (int)numImportQty.Value;
            decimal price = 0;
            decimal.TryParse(txtImportPrice.Text.Replace(",", "").Replace(".", "").Trim(), out price);

            var db = Program.BookManagementDb;
            var book = db.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return;

            // Tạo phiếu nhập
            var import = new BookImport
            {
                BookId = bookId,
                Quantity = qty,
                ImportPrice = price,
                Supplier = txtImportSupplier.Text.Trim(),
                ImportDate = DateTime.Now,
                ImportedBy = SessionManager.CurrentUser?.FullName ?? "Quản trị viên",
                Notes = txtImportNotes.Text.Trim()
            };
            db.BookImports.Add(import);

            // Tăng số lượng tồn kho của sách
            book.Quantity += qty;

            // Ghi nhật ký sửa đổi sách
            db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = bookId,
                ActionType = "Nhập kho",
                ChangeDescription = $"Nhập thêm {qty} cuốn từ '{import.Supplier}' (Đơn giá: {price:N0} đ). Tồn kho sau nhập: {book.Quantity}",
                ChangedBy = SessionManager.CurrentUser?.FullName ?? "Hệ thống",
                ChangedAt = DateTime.Now
            });

            db.SaveChanges();
            MessageBox.Show($"Nhập hàng thành công! Đã cộng thêm {qty} cuốn vào kho.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearImportInputs();
            LoadImportsData();
            LoadBooksData();
            LoadDashboardData();
        }

        private void dgvImportHistory_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvImportHistory.CurrentRow == null || dgvImportHistory.CurrentRow.Index < 0) return;

            try
            {
                int id = Convert.ToInt32(dgvImportHistory.CurrentRow.Cells["Id"].Value);
                var db = Program.BookManagementDb;
                var import = db.BookImports.FirstOrDefault(i => i.Id == id);
                if (import != null)
                {
                    cboImportBook.SelectedValue = import.BookId;
                    numImportQty.Value = Math.Min(numImportQty.Maximum, Math.Max(numImportQty.Minimum, import.Quantity));
                    txtImportPrice.Text = import.ImportPrice.ToString("0");
                    txtImportSupplier.Text = import.Supplier ?? "";
                    txtImportNotes.Text = import.Notes ?? "";
                }
            }
            catch { }
        }

        private void btnImportUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvImportHistory.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập hàng cần sửa từ danh sách bên dưới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboImportBook.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầu sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvImportHistory.CurrentRow.Cells["Id"].Value);
            int newBookId = Convert.ToInt32(cboImportBook.SelectedValue);
            int newQty = (int)numImportQty.Value;
            decimal newPrice = 0;
            decimal.TryParse(txtImportPrice.Text.Replace(",", "").Replace(".", "").Trim(), out newPrice);

            var db = Program.BookManagementDb;
            var import = db.BookImports.FirstOrDefault(i => i.Id == id);
            if (import == null)
            {
                MessageBox.Show("Không tìm thấy thông tin phiếu nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int oldBookId = import.BookId;
            int oldQty = import.Quantity;

            // Điều chỉnh tồn kho tương ứng
            if (oldBookId == newBookId)
            {
                var book = db.Books.FirstOrDefault(b => b.Id == newBookId);
                if (book != null)
                {
                    int diff = newQty - oldQty;
                    book.Quantity = Math.Max(0, book.Quantity + diff);
                }
            }
            else
            {
                var oldBook = db.Books.FirstOrDefault(b => b.Id == oldBookId);
                if (oldBook != null)
                {
                    oldBook.Quantity = Math.Max(0, oldBook.Quantity - oldQty);
                }

                var newBook = db.Books.FirstOrDefault(b => b.Id == newBookId);
                if (newBook != null)
                {
                    newBook.Quantity += newQty;
                }
            }

            // Cập nhật thông tin phiếu nhập
            import.BookId = newBookId;
            import.Quantity = newQty;
            import.ImportPrice = newPrice;
            import.Supplier = txtImportSupplier.Text.Trim();
            import.Notes = txtImportNotes.Text.Trim();

            // Ghi nhật ký audit log
            db.BookAuditLogs.Add(new BookAuditLog
            {
                BookId = newBookId,
                ActionType = "Sửa phiếu nhập",
                ChangeDescription = $"Sửa phiếu nhập #{import.Id}: {newQty} cuốn từ '{import.Supplier}' (Đơn giá: {newPrice:N0} đ).",
                ChangedBy = SessionManager.CurrentUser?.FullName ?? "Hệ thống",
                ChangedAt = DateTime.Now
            });

            db.SaveChanges();
            MessageBox.Show("Cập nhật phiếu nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearImportInputs();
            LoadImportsData();
            LoadBooksData();
            LoadDashboardData();
        }

        private void btnImportDelete_Click(object? sender, EventArgs e)
        {
            if (dgvImportHistory.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập hàng cần xóa từ danh sách bên dưới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvImportHistory.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này? Tồn kho của sách sẽ được trừ đi tương ứng.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var db = Program.BookManagementDb;
                var import = db.BookImports.FirstOrDefault(i => i.Id == id);
                if (import != null)
                {
                    var book = db.Books.FirstOrDefault(b => b.Id == import.BookId);
                    if (book != null)
                    {
                        book.Quantity = Math.Max(0, book.Quantity - import.Quantity);
                    }

                    db.BookAuditLogs.Add(new BookAuditLog
                    {
                        BookId = import.BookId,
                        ActionType = "Xóa phiếu nhập",
                        ChangeDescription = $"Xóa phiếu nhập #{import.Id} ({import.Quantity} cuốn từ '{import.Supplier}')",
                        ChangedBy = SessionManager.CurrentUser?.FullName ?? "Hệ thống",
                        ChangedAt = DateTime.Now
                    });

                    db.BookImports.Remove(import);
                    db.SaveChanges();
                    MessageBox.Show("Xóa phiếu nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearImportInputs();
                    LoadImportsData();
                    LoadBooksData();
                    LoadDashboardData();
                }
            }
        }

        private void ClearImportInputs()
        {
            numImportQty.Value = 10;
            txtImportPrice.Text = "50000";
            txtImportSupplier.Text = "NXB Kim Đồng";
            txtImportNotes.Clear();
            if (cboImportBook.Items.Count > 0)
            {
                cboImportBook.SelectedIndex = 0;
            }
        }

        // ==========================================
        // 6. QUẢN LÝ NGƯỜI DÙNG & PHÂN QUYỀN
        // ==========================================
        private void LoadUsersData()
        {
            try
            {
                var users = Program.BookManagementDb.Users.Select(u => new {
                    u.Id,
                    u.Username,
                    u.FullName,
                    u.Role,
                    u.Email,
                    u.Phone,
                    TrangThai = u.IsActive ? "Đang hoạt động" : "Bị khóa",
                    u.CreatedAt
                }).ToList();

                dgvUsers.DataSource = users;
                if (dgvUsers.Columns.Count > 0)
                {
                    dgvUsers.Columns["Id"].HeaderText = "ID";
                    dgvUsers.Columns["Username"].HeaderText = "Tên Đăng Nhập";
                    dgvUsers.Columns["FullName"].HeaderText = "Họ và Tên";
                    dgvUsers.Columns["Role"].HeaderText = "Vai Trò";
                    dgvUsers.Columns["Email"].HeaderText = "Email";
                    dgvUsers.Columns["Phone"].HeaderText = "Số ĐT";
                    dgvUsers.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    dgvUsers.Columns["CreatedAt"].HeaderText = "Ngày Tạo";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Users Load Error: " + ex.Message);
            }
        }

        private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsers.CurrentRow == null) return;
            var row = dgvUsers.CurrentRow;

            txtUserUsername.Text = row.Cells["Username"].Value?.ToString();
            txtUserFullName.Text = row.Cells["FullName"].Value?.ToString();
            cboUserRole.SelectedItem = row.Cells["Role"].Value?.ToString() ?? "User";
            txtUserEmail.Text = row.Cells["Email"].Value?.ToString();
            txtUserPhone.Text = row.Cells["Phone"].Value?.ToString();
            chkUserActive.Checked = row.Cells["TrangThai"].Value?.ToString() == "Đang hoạt động";
            txtUserPassword.Clear();
        }

        private void btnUserAdd_Click(object? sender, EventArgs e)
        {
            string username = txtUserUsername.Text.Trim();
            string password = txtUserPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var db = Program.BookManagementDb;
            if (db.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new User
            {
                Username = username,
                Password = PasswordHasher.Hash(password),   // Hash trước khi lưu
                FullName = txtUserFullName.Text.Trim(),
                Role = cboUserRole.SelectedItem?.ToString() ?? "User",
                Email = txtUserEmail.Text.Trim(),
                Phone = txtUserPhone.Text.Trim(),
                IsActive = chkUserActive.Checked,
                CreatedAt = DateTime.Now
            };

            db.Users.Add(newUser);
            db.SaveChanges();
            MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadUsersData();
        }

        private void btnUserUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["Id"].Value);

            var db = Program.BookManagementDb;
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.FullName = txtUserFullName.Text.Trim();
                user.Role = cboUserRole.SelectedItem?.ToString() ?? "User";
                user.Email = txtUserEmail.Text.Trim();
                user.Phone = txtUserPhone.Text.Trim();
                user.IsActive = chkUserActive.Checked;

                if (!string.IsNullOrEmpty(txtUserPassword.Text))
                {
                    user.Password = PasswordHasher.Hash(txtUserPassword.Text);  // Hash trước khi lưu
                }

                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsersData();
            }
        }

        private void btnUserDelete_Click(object? sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["Id"].Value);

            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.Id == id)
            {
                MessageBox.Show("Bạn không thể tự xóa tài khoản đang đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var db = Program.BookManagementDb;
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsersData();
                }
            }
        }

        // ==========================================
        // 7. BÁO CÁO THỐNG KÊ (NGÀY, THÁNG, NĂM)
        // ==========================================
        private void cboReportPeriod_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string mode = cboReportPeriod.SelectedItem?.ToString() ?? "";
            dtpReportDate.Visible = (mode == "Theo Ngày cụ thể");
            numReportMonth.Visible = (mode == "Theo Tháng");
            numReportYear.Visible = (mode == "Theo Tháng" || mode == "Theo Năm");
        }

        private void btnFilterReport_Click(object? sender, EventArgs e)
        {
            RunReports();
        }

        private void RunReports()
        {
            try
            {
                var db = Program.BookManagementDb;
                var borrowings = db.Borrowings.ToList();
                var imports = db.BookImports.ToList();
                var books = db.Books.ToList();
                var readers = db.Readers.ToList();

                string mode = cboReportPeriod.SelectedItem?.ToString() ?? "Tất cả thời gian";

                // Lọc theo kỳ
                IEnumerable<Borrowing> filteredBorrows = borrowings;
                IEnumerable<BookImport> filteredImports = imports;

                if (mode == "Hôm nay")
                {
                    var today = DateTime.Today;
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Date == today);
                    filteredImports = imports.Where(i => i.ImportDate.Date == today);
                }
                else if (mode == "Theo Ngày cụ thể")
                {
                    var selectedDate = dtpReportDate.Value.Date;
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Date == selectedDate);
                    filteredImports = imports.Where(i => i.ImportDate.Date == selectedDate);
                }
                else if (mode == "Theo Tháng")
                {
                    int month = (int)numReportMonth.Value;
                    int year = (int)numReportYear.Value;
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Month == month && b.BorrowDate.Year == year);
                    filteredImports = imports.Where(i => i.ImportDate.Month == month && i.ImportDate.Year == year);
                }
                else if (mode == "Theo Năm")
                {
                    int year = (int)numReportYear.Value;
                    filteredBorrows = borrowings.Where(b => b.BorrowDate.Year == year);
                    filteredImports = imports.Where(i => i.ImportDate.Year == year);
                }

                int totalBorrowQty = filteredBorrows.Sum(b => b.QualityBrrow);
                int totalReturned = filteredBorrows.Count(b => b.ReturnDate.HasValue);
                int totalOverdue = filteredBorrows.Count(b => !b.ReturnDate.HasValue && b.DueDate < DateTime.Now);
                decimal totalCost = filteredImports.Sum(i => i.Quantity * i.ImportPrice);

                lblRepBorrowCount.Text = $"LƯỢT MƯỢN\n{totalBorrowQty} cuốn";
                lblRepReturnCount.Text = $"ĐÃ TRẢ\n{totalReturned} phiếu";
                lblRepOverdueCount.Text = $"CHƯA TRẢ / QUÁ HẠN\n{totalOverdue} phiếu";
                lblRepTotalImportCost.Text = string.Format("TIỀN NHẬP SÁCH\n{0:N0} đ", totalCost);

                // Top 5 sách được mượn nhiều nhất
                var topBooks = filteredBorrows
                    .GroupBy(b => b.BookID)
                    .Select(g => new {
                        MaSach = g.Key,
                        TenSach = books.FirstOrDefault(x => x.Id == g.Key)?.Title ?? "(Đã xóa)",
                        TacGia = books.FirstOrDefault(x => x.Id == g.Key)?.Author ?? "(Không rõ)",
                        SoLuotMuon = g.Sum(x => x.QualityBrrow)
                    })
                    .OrderByDescending(x => x.SoLuotMuon)
                    .Take(5)
                    .ToList();

                dgvTopBooks.DataSource = topBooks;
                if (dgvTopBooks.Columns.Count > 0)
                {
                    dgvTopBooks.Columns["MaSach"].HeaderText = "Mã Sách";
                    dgvTopBooks.Columns["TenSach"].HeaderText = "Tên Sách";
                    dgvTopBooks.Columns["TacGia"].HeaderText = "Tác Giả";
                    dgvTopBooks.Columns["SoLuotMuon"].HeaderText = "Tổng Số Lượng Mượn";
                }

                // Chi tiết giao dịch
                var details = filteredBorrows.Select(b => new {
                    b.Id,
                    TenSach = books.FirstOrDefault(x => x.Id == b.BookID)?.Title ?? "(Đã xóa)",
                    DocGia = readers.FirstOrDefault(x => x.Id == b.ReaderID)?.Name ?? "(Đã xóa)",
                    b.BorrowDate,
                    b.DueDate,
                    b.ReturnDate,
                    SoLuong = b.QualityBrrow,
                    TrangThai = b.ReturnDate.HasValue ? "Đã trả" : "Chưa trả"
                }).ToList();

                dgvReportDetails.DataSource = details;
                if (dgvReportDetails.Columns.Count > 0)
                {
                    dgvReportDetails.Columns["Id"].HeaderText = "Mã Phiếu";
                    dgvReportDetails.Columns["TenSach"].HeaderText = "Tên Sách";
                    dgvReportDetails.Columns["DocGia"].HeaderText = "Độc Giả";
                    dgvReportDetails.Columns["BorrowDate"].HeaderText = "Ngày Mượn";
                    dgvReportDetails.Columns["DueDate"].HeaderText = "Hạn Trả";
                    dgvReportDetails.Columns["ReturnDate"].HeaderText = "Ngày Trả";
                    dgvReportDetails.Columns["SoLuong"].HeaderText = "Số Lượng";
                    dgvReportDetails.Columns["TrangThai"].HeaderText = "Trạng Thái";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập báo cáo: " + ex.Message);
            }
        }

        // ==========================================
        // 8. TÀI KHOẢN CỦA TÔI (PROFILE)
        // ==========================================
        private void LoadProfileData()
        {
            var user = SessionManager.CurrentUser;
            if (user != null)
            {
                txtProfUsername.Text = user.Username;
                txtProfRole.Text = user.Role;
                txtProfFullName.Text = user.FullName;
                txtProfEmail.Text = user.Email ?? "";
                txtProfPhone.Text = user.Phone ?? "";
            }
        }

        private void btnProfSave_Click(object? sender, EventArgs e)
        {
            var user = SessionManager.CurrentUser;
            if (user == null) return;

            var db = Program.BookManagementDb;
            var dbUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
            if (dbUser != null)
            {
                dbUser.FullName = txtProfFullName.Text.Trim();
                dbUser.Email = txtProfEmail.Text.Trim();
                dbUser.Phone = txtProfPhone.Text.Trim();

                db.SaveChanges();
                user.FullName = dbUser.FullName;
                user.Email = dbUser.Email;
                user.Phone = dbUser.Phone;

                ApplyPermissions();
                MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnChangePassword_Click(object? sender, EventArgs e)
        {
            var user = SessionManager.CurrentUser;
            if (user == null) return;

            string oldPass = txtOldPassword.Text;
            string newPass = txtNewPassword.Text;
            string confirmPass = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng điền đầy đủ mật khẩu cũ và mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var db = Program.BookManagementDb;
            var dbUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
            if (dbUser != null)
            {
                if (!PasswordHasher.Verify(oldPass, dbUser.Password))
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dbUser.Password = PasswordHasher.Hash(newPass);  // Hash mật khẩu mới
                db.SaveChanges();
                user.Password = dbUser.Password;  // Cập nhật session với hash mới

                txtOldPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class MouseWheelMessageFilter : IMessageFilter
    {
        private readonly MainForm _form;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_MOUSEHWHEEL = 0x020E;

        public MouseWheelMessageFilter(MainForm form)
        {
            _form = form;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEWHEEL || m.Msg == WM_MOUSEHWHEEL)
            {
                Panel? activeView = _form.CurrentActiveView;
                if (activeView != null && activeView.Visible && activeView.AutoScroll)
                {
                    Point screenPoint = Cursor.Position;
                    Point clientPoint = activeView.PointToClient(screenPoint);

                    if (activeView.ClientRectangle.Contains(clientPoint))
                    {
                        Control? ctl = Control.FromHandle(m.HWnd);

                        // If user is hovering over a DataGridView that actually has more rows than visible, let DataGridView scroll
                        if (ctl is DataGridView dgv && dgv.RowCount > dgv.DisplayedRowCount(false))
                        {
                            return false;
                        }

                        short delta = (short)((m.WParam.ToInt64() >> 16) & 0xffff);
                        int scrollStep = 60;

                        if (m.Msg == WM_MOUSEHWHEEL || (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                        {
                            int newX = -activeView.AutoScrollPosition.X + (delta > 0 ? scrollStep : -scrollStep);
                            if (newX < 0) newX = 0;
                            activeView.AutoScrollPosition = new Point(newX, -activeView.AutoScrollPosition.Y);
                        }
                        else
                        {
                            int newY = -activeView.AutoScrollPosition.Y - (delta > 0 ? scrollStep : -scrollStep);
                            if (newY < 0) newY = 0;
                            activeView.AutoScrollPosition = new Point(-activeView.AutoScrollPosition.X, newY);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
