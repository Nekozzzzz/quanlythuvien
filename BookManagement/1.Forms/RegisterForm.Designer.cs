using System;
using System.Drawing;
using System.Windows.Forms;

namespace BookManagement._1.Forms
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTitle;
        private TextBox txtUsername;
        private TextBox txtFullName;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPass;
        private Button btnRegister;
        private Button btnBackToLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Đăng Ký Tài Khoản Khách Hàng";
            this.Size = new Size(440, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Header Banner
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 75, BackColor = Color.FromArgb(0, 128, 128) };
            lblTitle = new Label
            {
                Text = "ĐĂNG KÝ KHÁCH HÀNG",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            int startY = 90, gap = 52, lblX = 35, txtX = 35, w = 355;

            // 1. Tên đăng nhập
            this.Controls.Add(new Label { Text = "Tên đăng nhập (*):", Location = new Point(lblX, startY), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 100, 100) });
            txtUsername = new TextBox { Location = new Point(txtX, startY + 20), Size = new Size(w, 25), Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtUsername);

            // 2. Họ và tên
            this.Controls.Add(new Label { Text = "Họ và tên (*):", Location = new Point(lblX, startY + gap), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 100, 100) });
            txtFullName = new TextBox { Location = new Point(txtX, startY + gap + 20), Size = new Size(w, 25), Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtFullName);

            // 3. Số điện thoại & Email
            this.Controls.Add(new Label { Text = "Số điện thoại:", Location = new Point(lblX, startY + gap * 2), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 100, 100) });
            txtPhone = new TextBox { Location = new Point(txtX, startY + gap * 2 + 20), Size = new Size(170, 25), Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtPhone);

            this.Controls.Add(new Label { Text = "Email:", Location = new Point(lblX + 185, startY + gap * 2), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 100, 100) });
            txtEmail = new TextBox { Location = new Point(txtX + 185, startY + gap * 2 + 20), Size = new Size(170, 25), Font = new Font("Segoe UI", 10F) };
            this.Controls.Add(txtEmail);

            // 4. Mật khẩu
            this.Controls.Add(new Label { Text = "Mật khẩu (*):", Location = new Point(lblX, startY + gap * 3), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 100, 100) });
            txtPassword = new TextBox { Location = new Point(txtX, startY + gap * 3 + 20), Size = new Size(w, 25), Font = new Font("Segoe UI", 10F), UseSystemPasswordChar = true };
            this.Controls.Add(txtPassword);

            // 5. Xác nhận mật khẩu
            this.Controls.Add(new Label { Text = "Xác nhận mật khẩu (*):", Location = new Point(lblX, startY + gap * 4), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 100, 100) });
            txtConfirmPass = new TextBox { Location = new Point(txtX, startY + gap * 4 + 20), Size = new Size(w, 25), Font = new Font("Segoe UI", 10F), UseSystemPasswordChar = true };
            this.Controls.Add(txtConfirmPass);

            // Nút Đăng Ký
            btnRegister = new Button
            {
                Text = "TẠO TÀI KHOẢN KHÁCH HÀNG",
                Location = new Point(txtX, startY + gap * 5 + 15),
                Size = new Size(w, 40),
                BackColor = Color.FromArgb(0, 128, 128),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);

            // Nút Quay lại Đăng nhập
            btnBackToLogin = new Button
            {
                Text = "Đã có tài khoản? Quay lại đăng nhập",
                Location = new Point(txtX, startY + gap * 5 + 62),
                Size = new Size(w, 30),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(0, 128, 128),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Underline),
                Cursor = Cursors.Hand
            };
            btnBackToLogin.FlatAppearance.BorderSize = 0;
            btnBackToLogin.Click += (s, e) => this.Close();
            this.Controls.Add(btnBackToLogin);
        }
    }
}
