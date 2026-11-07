using BookManagement._3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
// PasswordHasher nằm cùng namespace nên không cần using thêm

namespace BookManagement._4.Helpers
{
    public static class DbInitializer
    {
        public static void Initialize(BookManagementDbContext context)
        {
            try
            {
                // Tự động tạo các bảng nếu chưa có
                var createTablesSql = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Username VARCHAR(50) NOT NULL UNIQUE,
                        Password VARCHAR(255) NOT NULL,
                        FullName VARCHAR(100) NOT NULL,
                        Role VARCHAR(20) NOT NULL DEFAULT 'User',
                        Email VARCHAR(100),
                        Phone VARCHAR(20),
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                        IsActive TINYINT(1) DEFAULT 1
                    );

                    CREATE TABLE IF NOT EXISTS BookImports (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        BookId INT NOT NULL,
                        Quantity INT NOT NULL,
                        ImportPrice DECIMAL(18,2) DEFAULT 0,
                        Supplier VARCHAR(200),
                        ImportDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                        ImportedBy VARCHAR(100),
                        Notes TEXT,
                        FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS BookAuditLogs (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        BookId INT NOT NULL,
                        ActionType VARCHAR(50) NOT NULL,
                        ChangeDescription TEXT,
                        ChangedBy VARCHAR(100),
                        ChangedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE
                    );
                ";
                context.Database.ExecuteSqlRaw(createTablesSql);

                // Khởi tạo tài khoản mẫu nếu chưa có
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            Username = "admin",
                            Password = PasswordHasher.Hash("123456"),
                            FullName = "Trà Lê Thiên Lộc",
                            Role = "Admin",
                            Email = "admin@bookmanage.vn",
                            Phone = "0912345678",
                            CreatedAt = DateTime.Now.AddMonths(-3),
                            IsActive = true
                        },
                        new User
                        {
                            Username = "thuthu",
                            Password = PasswordHasher.Hash("123456"),
                            FullName = "Nguyễn Thị Thu Thư",
                            Role = "User",
                            Email = "thuthu@bookmanage.vn",
                            Phone = "0987654321",
                            CreatedAt = DateTime.Now.AddMonths(-1),
                            IsActive = true
                        }
                    );
                    context.SaveChanges();
                }

                // Khởi tạo phiếu nhập hàng mẫu
                if (!context.BookImports.Any() && context.Books.Any())
                {
                    var firstBook = context.Books.FirstOrDefault();
                    if (firstBook != null)
                    {
                        context.BookImports.AddRange(
                            new BookImport
                            {
                                BookId = firstBook.Id,
                                Quantity = 20,
                                ImportPrice = 85000,
                                Supplier = "NXB Kim Đồng",
                                ImportDate = DateTime.Now.AddDays(-15),
                                ImportedBy = "Trà Lê Thiên Lộc",
                                Notes = "Nhập bổ sung đợt đầu tháng"
                            },
                            new BookImport
                            {
                                BookId = firstBook.Id,
                                Quantity = 10,
                                ImportPrice = 90000,
                                Supplier = "Công ty CP Phát hành Sách Fahasa",
                                ImportDate = DateTime.Now.AddDays(-2),
                                ImportedBy = "Nguyễn Thị Thu Thư",
                                Notes = "Nhập hàng phục vụ bạn đọc kỳ thi"
                            }
                        );
                        context.SaveChanges();
                    }
                }

                // Khởi tạo nhật ký sửa đổi mẫu
                if (!context.BookAuditLogs.Any() && context.Books.Any())
                {
                    var book = context.Books.FirstOrDefault();
                    if (book != null)
                    {
                        context.BookAuditLogs.AddRange(
                            new BookAuditLog
                            {
                                BookId = book.Id,
                                ActionType = "Thêm mới sách",
                                ChangeDescription = $"Thêm mới đầu sách '{book.Title}' với số lượng ban đầu: {book.Quantity}",
                                ChangedBy = "Trà Lê Thiên Lộc",
                                ChangedAt = DateTime.Now.AddDays(-20)
                            },
                            new BookAuditLog
                            {
                                BookId = book.Id,
                                ActionType = "Nhập kho",
                                ChangeDescription = "Nhập bổ sung 10 cuốn từ NXB Kim Đồng",
                                ChangedBy = "Trà Lê Thiên Lộc",
                                ChangedAt = DateTime.Now.AddDays(-2)
                            }
                        );
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Tránh throw làm crash app nếu đã tồn tại
                System.Diagnostics.Debug.WriteLine("DbInitializer Error: " + ex.Message);
            }
        }
    }
}
