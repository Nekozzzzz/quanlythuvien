using System.Drawing;
using System.Windows.Forms;

namespace BookManagement._1.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Header & Sidebar
        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelContainer;
        private Label lblAppLogo;
        private Label lblAppSubtitle;
        private Button btnNavHome;
        private Button btnNavBorrow;
        private Button btnNavBooks;
        private Button btnNavReaders;
        private Button btnNavImports;
        private Button btnNavUsers;
        private Button btnNavReports;
        private Button btnNavProfile;
        private Button btnNavLogout;
        private Button btnNavExit;

        private Label lblPageHeaderTitle;
        private Label lblHeaderUserInfo;
        private Label lblHeaderClock;
        private System.Windows.Forms.Timer timerClock;

        // View 1: Dashboard
        private Panel viewDashboard;
        private Panel panelDashBanner;
        private Label lblBannerTitle;
        private Label lblBannerSub;
        private Panel cardDashRevenue;
        private Label lblCardCostVal;
        private Label lblCardCostTitle;
        private Label lblCardCostSub;
        private Panel cardDashOrders;
        private Label lblCardBorrowsVal;
        private Label lblCardBorrowsTitle;
        private Label lblCardBorrowsSub;
        private Panel cardDashProducts;
        private Label lblCardBooksVal;
        private Label lblCardBooksTitle;
        private Label lblCardBooksSub;
        private Panel cardDashCustomers;
        private Label lblCardReadersVal;
        private Label lblCardReadersTitle;
        private Label lblCardReadersSub;
        private Label lblQuickAccessTitle;
        private Button btnQuickBorrow;
        private Button btnQuickBooks;
        private Button btnQuickReaders;
        private Button btnQuickReports;
        private Button btnQuickImports;
        private Button btnQuickUsers;
        private Panel panelDashFooter;
        private Label lblFooterStatus;

        // View 2: Mượn Trả sách (POS)
        private Panel viewBorrow;
        private GroupBox grpBorrowInput;
        private TextBox txtBorrowBook;
        private Button btnChooseBook;
        private TextBox txtBorrowReader;
        private Button btnChooseReader;
        private NumericUpDown numBorrowQty;
        private DateTimePicker dtpBorrowDate;
        private DateTimePicker dtpDueDate;
        private Button btnActionBorrow;
        private Button btnActionReturn;
        private Button btnRefreshBorrow;
        private TextBox txtSearchBorrow;
        private Button btnSearchBorrow;
        private DataGridView dgvBorrowings;

        // View 3: Quản lý Sách & Tồn kho
        private Panel viewBooks;
        private GroupBox grpBookInput;
        private TextBox txtBookTitle;
        private TextBox txtBookAuthor;
        private TextBox txtBookPublisher;
        private NumericUpDown numBookYear;
        private NumericUpDown numBookQuantity;
        private Button btnBookAdd;
        private Button btnBookUpdate;
        private Button btnBookDelete;
        private Button btnBookClear;
        private TextBox txtSearchBook;
        private Button btnSearchBook;
        private Button btnRefreshBook;
        private DataGridView dgvBooks;
        private Label lblInventoryAlertBanner;

        // View 4: Quản lý Độc giả
        private Panel viewReaders;
        private GroupBox grpReaderInput;
        private TextBox txtReaderName;
        private TextBox txtReaderEmail;
        private TextBox txtReaderPhone;
        private Button btnReaderAdd;
        private Button btnReaderUpdate;
        private Button btnReaderDelete;
        private Button btnReaderClear;
        private TextBox txtSearchReader;
        private Button btnSearchReader;
        private DataGridView dgvReaders;

        // View 5: Quản lý Nhập hàng & Lịch sử
        private Panel viewImports;
        private TabControl tabControlImports;
        private TabPage tabNewImport;
        private TabPage tabAuditLogs;
        private GroupBox grpImportInput;
        private ComboBox cboImportBook;
        private NumericUpDown numImportQty;
        private TextBox txtImportPrice;
        private TextBox txtImportSupplier;
        private TextBox txtImportNotes;
        private Button btnExecuteImport;
        private Button btnImportUpdate;
        private Button btnImportDelete;
        private Button btnImportClear;
        private DataGridView dgvImportHistory;
        private DataGridView dgvAuditLogs;

        // View 6: Quản lý Người dùng (Admin only)
        private Panel viewUsers;
        private GroupBox grpUserInput;
        private TextBox txtUserUsername;
        private TextBox txtUserPassword;
        private TextBox txtUserFullName;
        private ComboBox cboUserRole;
        private TextBox txtUserEmail;
        private TextBox txtUserPhone;
        private CheckBox chkUserActive;
        private Button btnUserAdd;
        private Button btnUserUpdate;
        private Button btnUserDelete;
        private Button btnUserClear;
        private DataGridView dgvUsers;

        // View 7: Báo cáo Thống kê
        private Panel viewReports;
        private GroupBox grpReportFilter;
        private ComboBox cboReportPeriod;
        private DateTimePicker dtpReportDate;
        private NumericUpDown numReportMonth;
        private NumericUpDown numReportYear;
        private Button btnFilterReport;
        private Label lblRepBorrowCount;
        private Label lblRepReturnCount;
        private Label lblRepTotalImportCost;
        private Label lblRepOverdueCount;
        private DataGridView dgvTopBooks;
        private DataGridView dgvReportDetails;

        // View 8: Tài khoản của tôi
        private Panel viewProfile;
        private GroupBox grpProfileInfo;
        private TextBox txtProfUsername;
        private TextBox txtProfFullName;
        private TextBox txtProfEmail;
        private TextBox txtProfPhone;
        private TextBox txtProfRole;
        private Button btnProfSave;
        private GroupBox grpChangePassword;
        private TextBox txtOldPassword;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Button btnChangePassword;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // ===== Color Palette (matching the reference UI) =====
        // Sidebar: Teal gradient
        private static readonly Color SidebarBg = Color.FromArgb(0, 128, 128);           // Teal
        private static readonly Color SidebarDark = Color.FromArgb(0, 105, 105);
        private static readonly Color SidebarBtnHover = Color.FromArgb(0, 160, 160);
        private static readonly Color SidebarBtnActive = Color.FromArgb(0, 180, 170);
        // Header
        private static readonly Color HeaderBg = Color.FromArgb(245, 245, 245);
        // Cards
        private static readonly Color CardGreen = Color.FromArgb(200, 230, 201);          // Sản phẩm đã bán
        private static readonly Color CardYellow = Color.FromArgb(255, 241, 118);         // Lợi nhuận
        private static readonly Color CardBlue = Color.FromArgb(179, 198, 225);           // Khách hàng
        private static readonly Color CardOrange = Color.FromArgb(255, 204, 128);         // Đơn hàng
        // Accent
        private static readonly Color AccentTeal = Color.FromArgb(0, 150, 150);
        private static readonly Color BtnRed = Color.FromArgb(220, 53, 69);

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelSidebar = new Panel();
            lblAppLogo = new Label();
            lblAppSubtitle = new Label();
            btnNavHome = new Button();
            btnNavBorrow = new Button();
            btnNavBooks = new Button();
            btnNavReaders = new Button();
            btnNavImports = new Button();
            btnNavUsers = new Button();
            btnNavReports = new Button();
            btnNavProfile = new Button();
            btnNavLogout = new Button();
            btnNavExit = new Button();

            panelHeader = new Panel();
            lblPageHeaderTitle = new Label();
            lblHeaderUserInfo = new Label();
            lblHeaderClock = new Label();
            timerClock = new System.Windows.Forms.Timer(components);

            panelContainer = new Panel();

            // Setup Sidebar & Header & Timer
            SetupSidebar();
            SetupHeader();

            // Setup Sub Views
            SetupDashboardView();
            SetupBorrowView();
            SetupBooksView();
            SetupReadersView();
            SetupImportsView();
            SetupUsersView();
            SetupReportsView();
            SetupProfileView();

            // Form Properties
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 760);
            MinimumSize = new Size(1100, 680);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Sách & Thư viện";
            BackColor = Color.FromArgb(240, 240, 240);

            Controls.Add(panelContainer);
            Controls.Add(panelHeader);
            Controls.Add(panelSidebar);
        }

        // ====================================================
        //  SIDEBAR — Teal / Cyan theme
        // ====================================================
        private void SetupSidebar()
        {
            panelSidebar.BackColor = SidebarBg;
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Width = 210;

            // ---- Logo area (icon-like label) ----
            lblAppLogo = new Label();
            lblAppLogo.Text = "📚";
            lblAppLogo.Font = new Font("Segoe UI", 30F, FontStyle.Regular);
            lblAppLogo.ForeColor = Color.White;
            lblAppLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblAppLogo.Location = new Point(0, 8);
            lblAppLogo.Size = new Size(210, 66);

            lblAppSubtitle = new Label();
            lblAppSubtitle.Text = "QUẢN LÝ SÁCH";
            lblAppSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAppSubtitle.ForeColor = Color.White;
            lblAppSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            lblAppSubtitle.Location = new Point(0, 90);
            lblAppSubtitle.Size = new Size(210, 33);

            panelSidebar.Controls.Add(lblAppLogo);
            panelSidebar.Controls.Add(lblAppSubtitle);

            int startY = 120;
            int btnHeight = 45;
            int gap = 2;

            btnNavHome = CreateSidebarButton("🏠  Trang Chủ", startY);
            btnNavBorrow = CreateSidebarButton("🧾  Hóa Đơn Bán", startY + (btnHeight + gap) * 1);
            btnNavBooks = CreateSidebarButton("📦  Sản Phẩm", startY + (btnHeight + gap) * 2);
            btnNavImports = CreateSidebarButton("📥  Nhập Sản Phẩm", startY + (btnHeight + gap) * 3);
            btnNavReaders = CreateSidebarButton("👤  Khách Hàng", startY + (btnHeight + gap) * 4);
            btnNavUsers = CreateSidebarButton("👥  Quản Lý Tài Khoản", startY + (btnHeight + gap) * 5);
            btnNavReports = CreateSidebarButton("📊  Báo Cáo", startY + (btnHeight + gap) * 6);
            btnNavProfile = CreateSidebarButton("⚙️  Cài Đặt Tài Khoản", startY + (btnHeight + gap) * 7);

            btnNavLogout = CreateSidebarButton("➡️  Đăng Xuất", 0);
            btnNavLogout.Dock = DockStyle.Bottom;
            btnNavLogout.Size = new Size(210, 48);
            btnNavLogout.BackColor = SidebarBtnActive;
            btnNavLogout.ForeColor = Color.White;
            btnNavLogout.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            btnNavExit = new Button();
            btnNavExit.Visible = false;
            btnNavExit.Size = new Size(0, 0);

            panelSidebar.Controls.AddRange(new Control[] {
                btnNavHome, btnNavBorrow, btnNavBooks, btnNavReaders,
                btnNavImports, btnNavUsers, btnNavReports, btnNavProfile,
                btnNavLogout, btnNavExit
            });
        }

        private Button CreateSidebarButton(string text, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(0, y);
            btn.Size = new Size(210, 44);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = SidebarBtnHover;
            btn.FlatAppearance.MouseDownBackColor = SidebarBtnActive;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.Transparent;
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        // ====================================================
        //  HEADER — Light gray, user info & clock
        // ====================================================
        private void SetupHeader()
        {
            panelHeader.BackColor = HeaderBg;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 70;
            panelHeader.Padding = new Padding(20, 0, 20, 0);

            // Tiêu đề trang
            lblPageHeaderTitle = new Label();
            lblPageHeaderTitle.Text = "Quản Lý Cửa Hàng Sách";
            lblPageHeaderTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPageHeaderTitle.ForeColor = Color.FromArgb(50, 50, 50);
            lblPageHeaderTitle.Location = new Point(175, 8);
            lblPageHeaderTitle.AutoSize = true;

            // Thông tin user
            lblHeaderUserInfo = new Label();
            lblHeaderUserInfo.Text = "Xin chào:     Quản trị viên\nChức vụ:     Admin";
            lblHeaderUserInfo.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblHeaderUserInfo.ForeColor = Color.FromArgb(60, 60, 60);
            lblHeaderUserInfo.Location = new Point(175, 35);
            lblHeaderUserInfo.AutoSize = true;

            // Đồng hồ
            lblHeaderClock = new Label();
            lblHeaderClock.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblHeaderClock.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblHeaderClock.ForeColor = Color.FromArgb(80, 80, 80);
            lblHeaderClock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblHeaderClock.Location = new Point(1050, 10);
            lblHeaderClock.Size = new Size(210, 22);
            lblHeaderClock.TextAlign = ContentAlignment.TopRight;

            // Nút đóng (X) ở góc phải
            Button btnClose = new Button();
            btnClose.Text = "X";
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.BackColor = Color.FromArgb(220, 53, 69);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Size = new Size(35, 30);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(1235, 5);
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();

            panelHeader.Controls.Add(lblPageHeaderTitle);
            panelHeader.Controls.Add(lblHeaderUserInfo);
            panelHeader.Controls.Add(lblHeaderClock);
            panelHeader.Controls.Add(btnClose);

            timerClock = new System.Windows.Forms.Timer(components);
            timerClock.Interval = 1000;
            timerClock.Tick += (s, e) => {
                lblHeaderClock.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            };
            timerClock.Start();

            panelContainer = new Panel();
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.BackColor = Color.FromArgb(235, 235, 235);
        }

        // ====================================================
        //  VIEW 1: DASHBOARD — Colored cards
        // ====================================================
        private void SetupDashboardView()
        {
            viewDashboard = new Panel();
            viewDashboard.Dock = DockStyle.Fill;
            viewDashboard.AutoScroll = true;
            viewDashboard.AutoScrollMinSize = new Size(1100, 680);

            // Cards area
            int cardW = 300;
            int cardH = 120;
            int cardGap = 25;
            int startX = 30;
            int cardY = 20;

            // Card 1 — Sản phẩm đã bán (Green)
            cardDashOrders = CreateDashCard("Sản phẩm đã bán", "0 đơn", "📦", CardGreen, startX, cardY, cardW, cardH, out lblCardBorrowsTitle, out lblCardBorrowsVal, out lblCardBorrowsSub);

            // Card 2 — Doanh thu / Lợi nhuận (Yellow)
            cardDashRevenue = CreateDashCard("Lợi Nhuận", "0 đ", "💰", CardYellow, startX + cardW + cardGap, cardY, cardW, cardH, out lblCardCostTitle, out lblCardCostVal, out lblCardCostSub);

            // Card 3 — Tổng khách hàng (Blue)
            cardDashCustomers = CreateDashCard("Tổng khách hàng đăng ký", "0 khách", "👥", CardBlue, startX + (cardW + cardGap) * 2, cardY, cardW, cardH, out lblCardReadersTitle, out lblCardReadersVal, out lblCardReadersSub);

            // Card 4 — Tổng sản phẩm (Orange) — thêm 1 card
            cardDashProducts = CreateDashCard("Tổng Sản Phẩm", "0 SP", "📚", CardOrange, startX, cardY + cardH + 20, cardW, cardH, out lblCardBooksTitle, out lblCardBooksVal, out lblCardBooksSub);

            // Quick Access Title
            lblQuickAccessTitle = new Label();
            lblQuickAccessTitle.Text = "⚡ Truy Cập Nhanh";
            lblQuickAccessTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblQuickAccessTitle.ForeColor = Color.FromArgb(50, 50, 50);
            lblQuickAccessTitle.Location = new Point(30, 290);
            lblQuickAccessTitle.AutoSize = true;

            // 6 Quick Access Buttons
            int qW = 190;
            int qH = 80;
            int qGap = 18;
            int qY1 = 325;
            int qY2 = 325 + qH + qGap;

            btnQuickBorrow = CreateQuickBtn("🧾\nHóa Đơn Bán", AccentTeal, 30, qY1, qW, qH);
            btnQuickBooks = CreateQuickBtn("📦\nSản Phẩm", Color.FromArgb(33, 150, 83), 30 + (qW + qGap) * 1, qY1, qW, qH);
            btnQuickReaders = CreateQuickBtn("👤\nKhách Hàng", Color.FromArgb(52, 122, 180), 30 + (qW + qGap) * 2, qY1, qW, qH);
            btnQuickReports = CreateQuickBtn("📊\nBáo Cáo", Color.FromArgb(155, 89, 182), 30, qY2, qW, qH);
            btnQuickImports = CreateQuickBtn("📥\nNhập Hàng", Color.FromArgb(230, 126, 34), 30 + (qW + qGap) * 1, qY2, qW, qH);
            btnQuickUsers = CreateQuickBtn("👥\nNhân Viên", Color.FromArgb(44, 62, 80), 30 + (qW + qGap) * 2, qY2, qW, qH);

            // Footer
            panelDashFooter = new Panel();
            panelDashFooter.Dock = DockStyle.Bottom;
            panelDashFooter.Height = 30;
            panelDashFooter.BackColor = Color.FromArgb(220, 220, 220);
            lblFooterStatus = new Label();
            lblFooterStatus.Text = "🟢 Hệ thống sẵn sàng (v2.0)    |    CSDL: MySQL (bookmanagementdb)";
            lblFooterStatus.Font = new Font("Segoe UI", 8.5F);
            lblFooterStatus.ForeColor = Color.FromArgb(80, 80, 80);
            lblFooterStatus.Location = new Point(15, 6);
            lblFooterStatus.AutoSize = true;
            panelDashFooter.Controls.Add(lblFooterStatus);

            viewDashboard.Controls.AddRange(new Control[] {
                cardDashOrders, cardDashRevenue, cardDashCustomers, cardDashProducts,
                lblQuickAccessTitle,
                btnQuickBorrow, btnQuickBooks, btnQuickReaders,
                btnQuickReports, btnQuickImports, btnQuickUsers,
                panelDashFooter
            });

            panelContainer.Controls.Add(viewDashboard);
        }

        private Panel CreateDashCard(string title, string value, string icon, Color bgColor, int x, int y, int w, int h, out Label lblTitle, out Label lblVal, out Label lblSub)
        {
            Panel card = new Panel();
            card.BackColor = bgColor;
            card.Location = new Point(x, y);
            card.Size = new Size(w, h);

            lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 50);
            lblTitle.Location = new Point(15, 12);
            lblTitle.AutoSize = true;

            lblVal = new Label();
            lblVal.Text = value;
            lblVal.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblVal.ForeColor = Color.FromArgb(30, 30, 30);
            lblVal.Location = new Point(15, 45);
            lblVal.AutoSize = true;

            // Icon bên phải
            lblSub = new Label();
            lblSub.Text = icon;
            lblSub.Font = new Font("Segoe UI", 36F);
            lblSub.ForeColor = Color.FromArgb(60, 60, 60);
            lblSub.Location = new Point(w - 85, 20);
            lblSub.Size = new Size(75, 70);
            lblSub.TextAlign = ContentAlignment.MiddleCenter;

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblVal);
            card.Controls.Add(lblSub);
            return card;
        }

        private Button CreateQuickBtn(string text, Color bgColor, int x, int y, int w, int h)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(w, h);
            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            return btn;
        }

        // ====================================================
        //  VIEW 2: MƯỢN TRẢ SÁCH (POS)
        // ====================================================
        private void SetupBorrowView()
        {
            viewBorrow = new Panel();
            viewBorrow.AutoScroll = true;
            viewBorrow.AutoScrollMinSize = new Size(1100, 720);
            viewBorrow.Dock = DockStyle.Fill;
            viewBorrow.Visible = false;

            grpBorrowInput = new GroupBox();
            grpBorrowInput.Text = "  Thông Tin Mượn Trả Sách  ";
            grpBorrowInput.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpBorrowInput.ForeColor = AccentTeal;
            grpBorrowInput.Location = new Point(20, 15);
            grpBorrowInput.Size = new Size(1060, 140);
            grpBorrowInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label l1 = new Label { Text = "Sách:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 30), AutoSize = true };
            txtBorrowBook = new TextBox { Location = new Point(70, 27), Size = new Size(240, 25), ReadOnly = true, BackColor = Color.FromArgb(250, 250, 250) };
            btnChooseBook = new Button { Text = "Chọn", Location = new Point(315, 26), Size = new Size(90, 27), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnChooseBook.FlatAppearance.BorderSize = 0;

            Label l2 = new Label { Text = "Độc giả:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(405, 30), AutoSize = true };
            txtBorrowReader = new TextBox { Location = new Point(470, 27), Size = new Size(230, 25), ReadOnly = true, BackColor = Color.FromArgb(250, 250, 250) };
            btnChooseReader = new Button { Text = "Chọn", Location = new Point(705, 26), Size = new Size(90, 27), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnChooseReader.FlatAppearance.BorderSize = 0;

            Label l3 = new Label { Text = "Số lượng:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(795, 30), AutoSize = true };
            numBorrowQty = new NumericUpDown { Location = new Point(870, 27), Size = new Size(80, 25), Minimum = 1, Maximum = 100, Value = 1 };

            Label l4 = new Label { Text = "Ngày mượn:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 75), AutoSize = true };
            dtpBorrowDate = new DateTimePicker { Location = new Point(110, 72), Size = new Size(135, 25), Format = DateTimePickerFormat.Short };

            Label l5 = new Label { Text = "Hạn trả:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(260, 75), AutoSize = true };
            dtpDueDate = new DateTimePicker { Location = new Point(325, 72), Size = new Size(135, 25), Format = DateTimePickerFormat.Short };

            btnActionBorrow = new Button { Text = "➕ MƯỢN SÁCH", Location = new Point(470, 68), Size = new Size(145, 34), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnActionBorrow.FlatAppearance.BorderSize = 0;
            btnActionReturn = new Button { Text = "↩️ TRẢ SÁCH", Location = new Point(625, 68), Size = new Size(135, 34), BackColor = Color.FromArgb(33, 150, 83), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnActionReturn.FlatAppearance.BorderSize = 0;
            btnRefreshBorrow = new Button { Text = "🔄 Làm mới", Location = new Point(770, 68), Size = new Size(145, 34), BackColor = Color.FromArgb(200, 200, 200), FlatStyle = FlatStyle.Flat };
            btnRefreshBorrow.FlatAppearance.BorderSize = 0;

            grpBorrowInput.Controls.AddRange(new Control[] {
                l1, txtBorrowBook, btnChooseBook,
                l2, txtBorrowReader, btnChooseReader,
                l3, numBorrowQty,
                l4, dtpBorrowDate, l5, dtpDueDate,
                btnActionBorrow, btnActionReturn, btnRefreshBorrow
            });

            txtSearchBorrow = new TextBox { Location = new Point(20, 168), Size = new Size(300, 25), PlaceholderText = "Tìm theo tên sách hoặc độc giả..." };
            btnSearchBorrow = new Button { Text = "🔍 Tìm kiếm", Location = new Point(325, 167), Size = new Size(100, 27), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSearchBorrow.FlatAppearance.BorderSize = 0;

            dgvBorrowings = CreateStyledGrid();
            dgvBorrowings.Location = new Point(20, 205);
            dgvBorrowings.Size = new Size(1060, 480);
            dgvBorrowings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            viewBorrow.Controls.AddRange(new Control[] { grpBorrowInput, txtSearchBorrow, btnSearchBorrow, dgvBorrowings });
            panelContainer.Controls.Add(viewBorrow);
        }

        // ====================================================
        //  VIEW 3: QUẢN LÝ SÁCH & TỒN KHO
        // ====================================================
        private void SetupBooksView()
        {
            viewBooks = new Panel();
            viewBooks.Dock = DockStyle.Fill;
            viewBooks.Visible = false;
            viewBooks.AutoScroll = true;
            viewBooks.AutoScrollMinSize = new Size(1100, 720);

            lblInventoryAlertBanner = new Label();
            lblInventoryAlertBanner.Text = "📊 Hệ thống tự động cảnh báo đối với sản phẩm có tồn kho ≤ 5.";
            lblInventoryAlertBanner.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblInventoryAlertBanner.ForeColor = Color.FromArgb(180, 100, 0);
            lblInventoryAlertBanner.BackColor = Color.FromArgb(255, 243, 205);
            lblInventoryAlertBanner.Location = new Point(20, 10);
            lblInventoryAlertBanner.Size = new Size(1060, 22);
            lblInventoryAlertBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblInventoryAlertBanner.Padding = new Padding(5, 2, 0, 0);

            grpBookInput = new GroupBox();
            grpBookInput.Text = "  Thông Tin Sản Phẩm & Tồn Kho  ";
            grpBookInput.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpBookInput.ForeColor = AccentTeal;
            grpBookInput.Location = new Point(20, 38);
            grpBookInput.Size = new Size(1060, 125);
            grpBookInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label l1 = new Label { Text = "Tên sách:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 30), AutoSize = true };
            txtBookTitle = new TextBox { Location = new Point(90, 27), Size = new Size(240, 25) };

            Label l2 = new Label { Text = "Tác giả:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(345, 30), AutoSize = true };
            txtBookAuthor = new TextBox { Location = new Point(405, 27), Size = new Size(190, 25) };

            Label l3 = new Label { Text = "Nhà XB:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(605, 30), AutoSize = true };
            txtBookPublisher = new TextBox { Location = new Point(666, 27), Size = new Size(160, 25) };

            Label l4 = new Label { Text = "Năm:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(835, 30), AutoSize = true };
            numBookYear = new NumericUpDown { Location = new Point(880, 27), Size = new Size(70, 25), Minimum = 1900, Maximum = 2100, Value = 2024 };

            Label l5 = new Label { Text = "Tồn kho:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 78), AutoSize = true };
            numBookQuantity = new NumericUpDown { Location = new Point(90, 75), Size = new Size(90, 25), Minimum = 0, Maximum = 10000, Value = 10 };

            btnBookAdd = new Button { Text = "➕ Thêm", Location = new Point(200, 72), Size = new Size(100, 32), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnBookAdd.FlatAppearance.BorderSize = 0;
            btnBookUpdate = new Button { Text = "✏️ Cập nhật", Location = new Point(310, 72), Size = new Size(120, 32), BackColor = Color.FromArgb(33, 150, 83), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnBookUpdate.FlatAppearance.BorderSize = 0;
            btnBookDelete = new Button { Text = "🗑️ Xóa", Location = new Point(440, 72), Size = new Size(85, 32), BackColor = BtnRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnBookDelete.FlatAppearance.BorderSize = 0;
            btnBookClear = new Button { Text = "🔄 Làm mới", Location = new Point(535, 72), Size = new Size(145, 32), BackColor = Color.FromArgb(200, 200, 200), FlatStyle = FlatStyle.Flat };
            btnBookClear.FlatAppearance.BorderSize = 0;

            grpBookInput.Controls.AddRange(new Control[] {
                l1, txtBookTitle, l2, txtBookAuthor, l3, txtBookPublisher, l4, numBookYear,
                l5, numBookQuantity,
                btnBookAdd, btnBookUpdate, btnBookDelete, btnBookClear
            });

            txtSearchBook = new TextBox { Location = new Point(20, 175), Size = new Size(320, 25), PlaceholderText = "Tìm theo tên sách, tác giả, nhà xuất bản..." };
            btnSearchBook = new Button { Text = "🔍 Tìm", Location = new Point(345, 174), Size = new Size(80, 27), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSearchBook.FlatAppearance.BorderSize = 0;
            btnRefreshBook = new Button { Text = "🔄 Tải lại", Location = new Point(430, 174), Size = new Size(85, 27), BackColor = Color.FromArgb(200, 200, 200), FlatStyle = FlatStyle.Flat };
            btnRefreshBook.FlatAppearance.BorderSize = 0;

            dgvBooks = CreateStyledGrid();
            dgvBooks.Location = new Point(20, 210);
            dgvBooks.Size = new Size(1060, 475);
            dgvBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            viewBooks.Controls.AddRange(new Control[] { lblInventoryAlertBanner, grpBookInput, txtSearchBook, btnSearchBook, btnRefreshBook, dgvBooks });
            panelContainer.Controls.Add(viewBooks);
        }

        // ====================================================
        //  VIEW 4: QUẢN LÝ ĐỘC GIẢ / KHÁCH HÀNG
        // ====================================================
        private void SetupReadersView()
        {
            viewReaders = new Panel();
            viewReaders.Dock = DockStyle.Fill;
            viewReaders.Visible = false;
            viewReaders.AutoScroll = true;
            viewReaders.AutoScrollMinSize = new Size(1100, 720);

            grpReaderInput = new GroupBox();
            grpReaderInput.Text = "  Thông Tin Khách Hàng  ";
            grpReaderInput.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpReaderInput.ForeColor = AccentTeal;
            grpReaderInput.Location = new Point(20, 20);
            grpReaderInput.Size = new Size(1060, 115);
            grpReaderInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label l1 = new Label { Text = "Họ và tên:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 32), AutoSize = true };
            txtReaderName = new TextBox { Location = new Point(95, 29), Size = new Size(220, 25) };

            Label l2 = new Label { Text = "Email:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(335, 32), AutoSize = true };
            txtReaderEmail = new TextBox { Location = new Point(385, 29), Size = new Size(220, 25) };

            Label l3 = new Label { Text = "Số ĐT:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(625, 32), AutoSize = true };
            txtReaderPhone = new TextBox { Location = new Point(680, 29), Size = new Size(180, 25) };

            btnReaderAdd = new Button { Text = "➕ Thêm", Location = new Point(95, 70), Size = new Size(100, 32), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnReaderAdd.FlatAppearance.BorderSize = 0;
            btnReaderUpdate = new Button { Text = "✏️ Sửa", Location = new Point(205, 70), Size = new Size(95, 32), BackColor = Color.FromArgb(33, 150, 83), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnReaderUpdate.FlatAppearance.BorderSize = 0;
            btnReaderDelete = new Button { Text = "🗑️ Xóa", Location = new Point(310, 70), Size = new Size(85, 32), BackColor = BtnRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnReaderDelete.FlatAppearance.BorderSize = 0;
            btnReaderClear = new Button { Text = "🔄 Làm mới", Location = new Point(405, 70), Size = new Size(145, 32), BackColor = Color.FromArgb(200, 200, 200), FlatStyle = FlatStyle.Flat };
            btnReaderClear.FlatAppearance.BorderSize = 0;

            grpReaderInput.Controls.AddRange(new Control[] {
                l1, txtReaderName, l2, txtReaderEmail, l3, txtReaderPhone,
                btnReaderAdd, btnReaderUpdate, btnReaderDelete, btnReaderClear
            });

            txtSearchReader = new TextBox { Location = new Point(20, 148), Size = new Size(320, 25), PlaceholderText = "Tìm theo tên, email, số điện thoại..." };
            btnSearchReader = new Button { Text = "🔍 Tìm", Location = new Point(345, 147), Size = new Size(80, 27), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSearchReader.FlatAppearance.BorderSize = 0;

            dgvReaders = CreateStyledGrid();
            dgvReaders.Location = new Point(20, 185);
            dgvReaders.Size = new Size(1060, 500);
            dgvReaders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            viewReaders.Controls.AddRange(new Control[] { grpReaderInput, txtSearchReader, btnSearchReader, dgvReaders });
            panelContainer.Controls.Add(viewReaders);
        }

        // ====================================================
        //  VIEW 5: NHẬP HÀNG & LỊCH SỬ
        // ====================================================
        private void SetupImportsView()
        {
            viewImports = new Panel();
            viewImports.Dock = DockStyle.Fill;
            viewImports.Visible = false;
            viewImports.AutoScroll = true;
            viewImports.AutoScrollMinSize = new Size(1100, 720);
            tabControlImports = new TabControl();
            tabControlImports.Dock = DockStyle.Fill;
            tabControlImports.Font = new Font("Segoe UI", 9.5F);
            tabNewImport = new TabPage("📥 Nhập Hàng Mới & Lịch Sử");
            tabNewImport.AutoScroll = true;
            tabNewImport.AutoScrollMinSize = new Size(1060, 680);
            tabAuditLogs = new TabPage("📝 Nhật Ký Thay Đổi (Audit Log)");
            tabAuditLogs.AutoScroll = true;
            tabAuditLogs.AutoScrollMinSize = new Size(1060, 680);
            // Tab 1: Nhập hàng
            grpImportInput = new GroupBox();
            grpImportInput.Text = "  Tạo Phiếu Nhập Hàng  ";
            grpImportInput.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpImportInput.ForeColor = AccentTeal;
            grpImportInput.Location = new Point(15, 15);
            grpImportInput.Size = new Size(1040, 125);
            grpImportInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Label l1 = new Label { Text = "Sách:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 30), AutoSize = true };
            cboImportBook = new ComboBox { Location = new Point(65, 27), Size = new Size(240, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            Label l2 = new Label { Text = "Số lượng:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(320, 30), AutoSize = true };
            numImportQty = new NumericUpDown { Location = new Point(395, 27), Size = new Size(85, 25), Minimum = 1, Maximum = 10000, Value = 10 };
            Label l3 = new Label { Text = "Giá nhập (đ):", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(485, 30), AutoSize = true };
            txtImportPrice = new TextBox { Location = new Point(580, 27), Size = new Size(130, 25), Text = "50000" };
            Label l4 = new Label { Text = "Nhà cung cấp:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(720, 30), AutoSize = true };
            txtImportSupplier = new TextBox { Location = new Point(825, 27), Size = new Size(195, 25), Text = "NXB Kim Đồng" };
            Label l5 = new Label { Text = "Ghi chú:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 75), AutoSize = true };
            txtImportNotes = new TextBox { Location = new Point(80, 72), Size = new Size(340, 25) };
            btnExecuteImport = new Button { Text = "📦 Nhập hàng", Location = new Point(435, 68), Size = new Size(140, 34), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnExecuteImport.FlatAppearance.BorderSize = 0;
            btnImportUpdate = new Button { Text = "✏️ Cập nhật", Location = new Point(585, 68), Size = new Size(125, 34), BackColor = Color.FromArgb(33, 150, 83), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnImportUpdate.FlatAppearance.BorderSize = 0;
            btnImportDelete = new Button { Text = "🗑️ Xóa", Location = new Point(720, 68), Size = new Size(110, 34), BackColor = BtnRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnImportDelete.FlatAppearance.BorderSize = 0;
            btnImportClear = new Button { Text = "🔄 Làm mới", Location = new Point(840, 68), Size = new Size(110, 34), BackColor = Color.FromArgb(200, 200, 200), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F) };
            btnImportClear.FlatAppearance.BorderSize = 0;
            grpImportInput.Controls.AddRange(new Control[] {
        l1, cboImportBook, l2, numImportQty, l3, txtImportPrice, l4, txtImportSupplier,
        l5, txtImportNotes, btnExecuteImport, btnImportUpdate, btnImportDelete, btnImportClear
    });
            dgvImportHistory = CreateStyledGrid();
            dgvImportHistory.Location = new Point(15, 155);
            dgvImportHistory.Size = new Size(1040, 480);
            dgvImportHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabNewImport.Controls.Add(grpImportInput);
            tabNewImport.Controls.Add(dgvImportHistory);
            // Tab 2: Audit Logs
            dgvAuditLogs = CreateStyledGrid();
            dgvAuditLogs.Dock = DockStyle.Fill;
            tabAuditLogs.Controls.Add(dgvAuditLogs);
            tabControlImports.TabPages.Add(tabNewImport);
            tabControlImports.TabPages.Add(tabAuditLogs);
            viewImports.Controls.Add(tabControlImports);
            panelContainer.Controls.Add(viewImports);
        }

        // ====================================================
        //  VIEW 6: QUẢN LÝ NHÂN VIÊN / NGƯỜI DÙNG
        // ====================================================
        private void SetupUsersView()
        {
            viewUsers = new Panel();
            viewUsers.Dock = DockStyle.Fill;
            viewUsers.Visible = false;
            viewUsers.AutoScroll = true;
            viewUsers.AutoScrollMinSize = new Size(1100, 720);

            grpUserInput = new GroupBox();
            grpUserInput.Text = "  Quản Lý Tài Khoản & Phân Quyền (Admin)  ";
            grpUserInput.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpUserInput.ForeColor = AccentTeal;
            grpUserInput.Location = new Point(20, 15);
            grpUserInput.Size = new Size(1060, 135);
            grpUserInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label l1 = new Label { Text = "Tên đăng nhập:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 30), AutoSize = true };
            txtUserUsername = new TextBox { Location = new Point(130, 27), Size = new Size(160, 25) };

            Label l2 = new Label { Text = "Mật khẩu:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(305, 30), AutoSize = true };
            txtUserPassword = new TextBox { Location = new Point(380, 27), Size = new Size(160, 25) };

            Label l3 = new Label { Text = "Họ và tên:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(555, 30), AutoSize = true };
            txtUserFullName = new TextBox { Location = new Point(630, 27), Size = new Size(180, 25) };

            Label l4 = new Label { Text = "Vai trò:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(825, 30), AutoSize = true };
            cboUserRole = new ComboBox { Location = new Point(880, 27), Size = new Size(115, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cboUserRole.Items.AddRange(new object[] { "Admin", "User" });
            cboUserRole.SelectedIndex = 1;

            Label l5 = new Label { Text = "Email:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 75), AutoSize = true };
            txtUserEmail = new TextBox { Location = new Point(70, 72), Size = new Size(215, 25) };

            Label l6 = new Label { Text = "Số ĐT:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(305, 75), AutoSize = true };
            txtUserPhone = new TextBox { Location = new Point(360, 72), Size = new Size(175, 25) };

            chkUserActive = new CheckBox { Text = "Đang hoạt động", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(555, 74), Checked = true, AutoSize = true };

            btnUserAdd = new Button { Text = "➕ Thêm", Location = new Point(695, 68), Size = new Size(90, 32), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnUserAdd.FlatAppearance.BorderSize = 0;
            btnUserUpdate = new Button { Text = "✏️ Cập nhật", Location = new Point(795, 68), Size = new Size(120, 32), BackColor = Color.FromArgb(33, 150, 83), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnUserUpdate.FlatAppearance.BorderSize = 0;
            btnUserDelete = new Button { Text = "🗑️ Xóa", Location = new Point(925, 68), Size = new Size(80, 32), BackColor = BtnRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnUserDelete.FlatAppearance.BorderSize = 0;

            grpUserInput.Controls.AddRange(new Control[] {
                l1, txtUserUsername, l2, txtUserPassword, l3, txtUserFullName, l4, cboUserRole,
                l5, txtUserEmail, l6, txtUserPhone, chkUserActive,
                btnUserAdd, btnUserUpdate, btnUserDelete
            });

            dgvUsers = CreateStyledGrid();
            dgvUsers.Location = new Point(20, 165);
            dgvUsers.Size = new Size(1060, 520);
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            viewUsers.Controls.AddRange(new Control[] { grpUserInput, dgvUsers });
            panelContainer.Controls.Add(viewUsers);
        }

        // ====================================================
        //  VIEW 7: BÁO CÁO THỐNG KÊ
        // ====================================================
        private void SetupReportsView()
        {
            viewReports = new Panel();
            viewReports.AutoScroll = true;
            viewReports.AutoScrollMinSize = new Size(1100, 800);
            viewReports.Dock = DockStyle.Fill;
            viewReports.Visible = false;

            grpReportFilter = new GroupBox();
            grpReportFilter.Text = "  Bộ Lọc Báo Cáo  ";
            grpReportFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpReportFilter.ForeColor = AccentTeal;
            grpReportFilter.Location = new Point(20, 15);
            grpReportFilter.Size = new Size(1060, 80);
            grpReportFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label l1 = new Label { Text = "Kỳ báo cáo:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(20, 32), AutoSize = true };
            cboReportPeriod = new ComboBox { Location = new Point(110, 29), Size = new Size(180, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cboReportPeriod.Items.AddRange(new object[] { "Tất cả thời gian", "Hôm nay", "Theo Ngày cụ thể", "Theo Tháng", "Theo Năm" });
            cboReportPeriod.SelectedIndex = 0;

            dtpReportDate = new DateTimePicker { Location = new Point(300, 29), Size = new Size(130, 25), Format = DateTimePickerFormat.Short, Visible = false };
            numReportMonth = new NumericUpDown { Location = new Point(415, 29), Size = new Size(60, 25), Minimum = 1, Maximum = 12, Value = System.DateTime.Now.Month, Visible = false };
            numReportYear = new NumericUpDown { Location = new Point(485, 29), Size = new Size(80, 25), Minimum = 2000, Maximum = 2100, Value = System.DateTime.Now.Year, Visible = false };

            btnFilterReport = new Button { Text = "📊 XEM THỐNG KÊ", Location = new Point(580, 26), Size = new Size(180, 32), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            btnFilterReport.FlatAppearance.BorderSize = 0;

            grpReportFilter.Controls.AddRange(new Control[] { l1, cboReportPeriod, dtpReportDate, numReportMonth, numReportYear, btnFilterReport });

            // 4 Mini Summary Labels
            Panel pnlStats = new Panel { Location = new Point(20, 105), Size = new Size(1060, 75), BackColor = Color.White, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            lblRepBorrowCount = new Label { Text = "LƯỢT MƯỢN\n0", Location = new Point(25, 12), Size = new Size(200, 50), Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = AccentTeal };
            lblRepReturnCount = new Label { Text = "ĐÃ TRẢ\n0", Location = new Point(260, 12), Size = new Size(200, 50), Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(33, 150, 83) };
            lblRepOverdueCount = new Label { Text = "CHƯA TRẢ / QUÁ HẠN\n0", Location = new Point(500, 12), Size = new Size(220, 50), Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = BtnRed };
            lblRepTotalImportCost = new Label { Text = "TIỀN NHẬP SÁCH\n0 đ", Location = new Point(770, 12), Size = new Size(250, 50), Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(155, 89, 182) };
            pnlStats.Controls.AddRange(new Control[] { lblRepBorrowCount, lblRepReturnCount, lblRepOverdueCount, lblRepTotalImportCost });

            Label lblTop = new Label { Text = "🏆 TOP 5 SÁCH ĐƯỢC MƯỢN NHIỀU NHẤT", Location = new Point(20, 195), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 50, 50), AutoSize = true };

            dgvTopBooks = CreateStyledGrid();
            dgvTopBooks.Location = new Point(20, 220);
            dgvTopBooks.Size = new Size(1060, 180);
            dgvTopBooks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            Label lblDetails = new Label { Text = "📋 DANH SÁCH GIAO DỊCH", Location = new Point(20, 415), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 50, 50), AutoSize = true };

            dgvReportDetails = CreateStyledGrid();
            dgvReportDetails.Location = new Point(20, 440);
            dgvReportDetails.Size = new Size(1060, 320);
            dgvReportDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            viewReports.Controls.AddRange(new Control[] { grpReportFilter, pnlStats, lblTop, dgvTopBooks, lblDetails, dgvReportDetails });
            panelContainer.Controls.Add(viewReports);
        }

        // ====================================================
        //  VIEW 8: HỒ SƠ CÁ NHÂN
        // ====================================================
        private void SetupProfileView()
        {
            viewProfile = new Panel();
            viewProfile.Dock = DockStyle.Fill;
            viewProfile.Visible = false;
            viewProfile.AutoScroll = true;
            viewProfile.AutoScrollMinSize = new Size(1100, 700);

            grpProfileInfo = new GroupBox();
            grpProfileInfo.Text = "  Thông Tin Cá Nhân  ";
            grpProfileInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpProfileInfo.ForeColor = AccentTeal;
            grpProfileInfo.Location = new Point(40, 30);
            grpProfileInfo.Size = new Size(460, 340);

            Label l1 = new Label { Text = "Tên đăng nhập:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 42), AutoSize = true };
            txtProfUsername = new TextBox { Location = new Point(145, 39), Size = new Size(290, 25), ReadOnly = true, BackColor = Color.FromArgb(240, 240, 240) };

            Label l2 = new Label { Text = "Vai trò:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 92), AutoSize = true };
            txtProfRole = new TextBox { Location = new Point(145, 89), Size = new Size(290, 25), ReadOnly = true, BackColor = Color.FromArgb(240, 240, 240) };

            Label l3 = new Label { Text = "Họ và tên:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 142), AutoSize = true };
            txtProfFullName = new TextBox { Location = new Point(145, 139), Size = new Size(290, 25) };

            Label l4 = new Label { Text = "Email:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 192), AutoSize = true };
            txtProfEmail = new TextBox { Location = new Point(145, 189), Size = new Size(290, 25) };

            Label l5 = new Label { Text = "Số điện thoại:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 242), AutoSize = true };
            txtProfPhone = new TextBox { Location = new Point(145, 239), Size = new Size(290, 25) };

            btnProfSave = new Button { Text = "💾 Lưu Thông Tin", Location = new Point(145, 285), Size = new Size(180, 36), BackColor = AccentTeal, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            btnProfSave.FlatAppearance.BorderSize = 0;

            grpProfileInfo.Controls.AddRange(new Control[] {
                l1, txtProfUsername, l2, txtProfRole, l3, txtProfFullName, l4, txtProfEmail, l5, txtProfPhone, btnProfSave
            });

            grpChangePassword = new GroupBox();
            grpChangePassword.Text = "  Đổi Mật Khẩu  ";
            grpChangePassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpChangePassword.ForeColor = AccentTeal;
            grpChangePassword.Location = new Point(530, 30);
            grpChangePassword.Size = new Size(460, 260);

            Label p1 = new Label { Text = "Mật khẩu hiện tại:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 47), AutoSize = true };
            txtOldPassword = new TextBox { Location = new Point(165, 44), Size = new Size(270, 25), UseSystemPasswordChar = true };

            Label p2 = new Label { Text = "Mật khẩu mới:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 97), AutoSize = true };
            txtNewPassword = new TextBox { Location = new Point(165, 94), Size = new Size(270, 25), UseSystemPasswordChar = true };

            Label p3 = new Label { Text = "Xác nhận mật khẩu:", Font = new Font("Segoe UI", 9F), ForeColor = Color.Black, Location = new Point(25, 147), AutoSize = true };
            txtConfirmPassword = new TextBox { Location = new Point(165, 144), Size = new Size(270, 25), UseSystemPasswordChar = true };

            btnChangePassword = new Button { Text = "🔒 Đổi Mật Khẩu", Location = new Point(165, 195), Size = new Size(180, 36), BackColor = Color.FromArgb(33, 150, 83), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            btnChangePassword.FlatAppearance.BorderSize = 0;

            grpChangePassword.Controls.AddRange(new Control[] {
                p1, txtOldPassword, p2, txtNewPassword, p3, txtConfirmPassword, btnChangePassword
            });

            viewProfile.Controls.Add(grpProfileInfo);
            viewProfile.Controls.Add(grpChangePassword);
            panelContainer.Controls.Add(viewProfile);
        }

        // ====================================================
        //  Helper: Styled DataGridView
        // ====================================================
        private DataGridView CreateStyledGrid()
        {
            var dgv = new DataGridView();
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;

            // Header style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = SidebarBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 38;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row style
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            dgv.RowTemplate.Height = 32;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 250);

            // Selection
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(178, 223, 219);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.GridColor = Color.FromArgb(220, 220, 220);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            return dgv;
        }
    }
}
