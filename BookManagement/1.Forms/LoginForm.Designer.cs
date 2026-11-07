using System.Drawing;
using System.Windows.Forms;

namespace BookManagement._1.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin;
        private Button btnExit;

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
            panelHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnLogin = new Button();
            btnExit = new Button();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(0, 128, 128);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(480, 127);
            panelHeader.TabIndex = 8;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(200, 230, 230);
            lblSubtitle.Location = new Point(31, 73);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(233, 21);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "Đăng nhập để truy cập hệ thống";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(29, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(264, 37);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "📚  QUẢN LÝ SÁCH";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(0, 100, 100);
            lblUsername.Location = new Point(40, 153);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(133, 23);
            lblUsername.TabIndex = 7;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 11F);
            txtUsername.Location = new Point(40, 187);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(399, 32);
            txtUsername.TabIndex = 6;
            txtUsername.Text = "";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(0, 100, 100);
            lblPassword.Location = new Point(40, 247);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(91, 23);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(40, 280);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(399, 32);
            txtPassword.TabIndex = 4;
            txtPassword.Text = "";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Font = new Font("Segoe UI", 9F);
            chkShowPassword.ForeColor = Color.FromArgb(80, 80, 80);
            chkShowPassword.Location = new Point(40, 327);
            chkShowPassword.Margin = new Padding(3, 4, 3, 4);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(148, 24);
            chkShowPassword.TabIndex = 3;
            chkShowPassword.Text = "Hiển thị mật khẩu";
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 128, 128);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 373);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(263, 53);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(200, 200, 200);
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(60, 60, 60);
            btnExit.Location = new Point(314, 373);
            btnExit.Margin = new Padding(3, 4, 3, 4);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(126, 53);
            btnExit.TabIndex = 1;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            //
            // Register
            //
            LinkLabel lnkRegister = new LinkLabel();
            lnkRegister.Text = "Chưa có tài khoản? Đăng ký ngay";
            lnkRegister.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lnkRegister.LinkColor = Color.FromArgb(0, 128, 128);
            lnkRegister.AutoSize = true;
            lnkRegister.Location = new Point(120, 442);
            lnkRegister.LinkClicked += (s, e) => {
                RegisterForm regForm = new RegisterForm();
                regForm.ShowDialog();
            };
            this.Controls.Add(lnkRegister);
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(480, 490);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(chkShowPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập Hệ Thống";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
