using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookManagement
{
    public class BookManagementDbContextFactory : IDesignTimeDbContextFactory<BookManagementDbContext>
    {
        public BookManagementDbContext CreateDbContext(string[] args)
        {
            // Đọc chuỗi kết nối từ appsettings.json thay vì hardcode
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Không tìm thấy 'DefaultConnection' trong appsettings.json. " +
                    "Vui lòng kiểm tra file cấu hình.");

            var optionsBuilder = new DbContextOptionsBuilder<BookManagementDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new BookManagementDbContext(optionsBuilder.Options);
        }
    }
}
