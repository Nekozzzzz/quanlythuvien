using BookManagement._3.Models;
using BookManagement._4.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BookManagement._1.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPass = txtConfirmPass.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các thông tin bắt buộc (*)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var db = Program.BookManagementDb;

                // Kiểm tra tên đăng nhập đã tồn tại chưa
                if (db.Users.Any(u => u.Username.ToLower() == username.ToLower()))
                {
                    MessageBox.Show("Tên đăng nhập này đã được sử dụng. Vui lòng chọn tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                // 1. Tạo tài khoản User với quyền Customer
                var newUser = new User
                {
                    Username = username,
                    Password = PasswordHasher.Hash(password),
                    FullName = fullName,
                    Role = "Customer",
                    Phone = phone,
                    Email = email,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };
                db.Users.Add(newUser);

                // 2. Tự động tạo bản ghi Reader (Độc giả) tương ứng nếu chưa có
                var existingReader = db.Readers.FirstOrDefault(r => 
                    (!string.IsNullOrEmpty(phone) && r.Phone == phone) || 
                    (!string.IsNullOrEmpty(email) && r.Email == email));

                if (existingReader == null)
                {
                    var newReader = new Reader
                    {
                        Name = fullName,
                        Phone = phone,
                        Email = email
                    };
                    db.Readers.Add(newReader);
                }

                db.SaveChanges();

                MessageBox.Show("Đăng ký tài khoản Khách hàng thành công!\nBạn có thể đăng nhập ngay bây giờ.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
