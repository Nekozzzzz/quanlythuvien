using BookManagement._1.Forms;
using BookManagement._4.Helpers;
using System;
using System.Windows.Forms;

namespace BookManagement
{
    internal static class Program
    {
        public static BookManagementDbContext BookManagementDb = null!;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            AppPaths.EnsureDataFolder();

            // Khởi tạo DbContext và cơ sở dữ liệu
            BookManagementDbContextFactory factory = new BookManagementDbContextFactory();
            BookManagementDb = factory.CreateDbContext(null);
            DbInitializer.Initialize(BookManagementDb);

            // Vòng lặp Đăng nhập / Đăng xuất
            while (true)
            {
                using (var loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        break; // Thoát ứng dụng nếu hủy đăng nhập
                    }
                }

                using (var mainForm = new MainForm())
                {
                    Application.Run(mainForm);
                    if (!mainForm.IsLoggedOut)
                    {
                        break; // Người dùng đóng form (không phải ấn đăng xuất) thì kết thúc
                    }
                }
            }
        }
    }
}
