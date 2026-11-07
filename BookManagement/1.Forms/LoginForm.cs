using BookManagement._4.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BookManagement._1.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = Program.BookManagementDb.Users.FirstOrDefault(u => u.Username == username);

                if (user == null || !PasswordHasher.Verify(password, user.Password))
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                if (!user.IsActive)
                {
                    MessageBox.Show("Tài khoản này hiện đang bị khóa. Vui lòng liên hệ Quản trị viên!", "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // Ghi nhận phiên đăng nhập
                SessionManager.CurrentUser = user;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
