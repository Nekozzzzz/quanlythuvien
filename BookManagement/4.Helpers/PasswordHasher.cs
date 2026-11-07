using System;
using System.Security.Cryptography;
using System.Text;

namespace BookManagement._4.Helpers
{
    /// <summary>
    /// Tiện ích hash và xác thực mật khẩu sử dụng SHA-256 + Salt ngẫu nhiên.
    /// Định dạng lưu trữ: "base64(salt):base64(hash)"
    /// Tương thích ngược: nếu không có dấu ':', xem là mật khẩu plain text cũ.
    /// </summary>
    public static class PasswordHasher
    {
        private const int SaltSize = 32; // 256-bit salt

        /// <summary>Hash mật khẩu thuần văn bản thành chuỗi "salt:hash".</summary>
        public static string Hash(string plainPassword)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Compute(plainPassword, salt);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Xác thực mật khẩu với giá trị đã lưu.
        /// Hỗ trợ cả plain text cũ (migration) lẫn hashed mới.
        /// </summary>
        public static bool Verify(string plainPassword, string stored)
        {
            if (string.IsNullOrEmpty(stored)) return false;

            // Mật khẩu cũ chưa được hash (không có ':')
            if (!stored.Contains(':'))
                return plainPassword == stored;

            var parts = stored.Split(':', 2);
            if (parts.Length != 2) return false;

            try
            {
                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] expected = Convert.FromBase64String(parts[1]);
                byte[] actual = Compute(plainPassword, salt);
                // So sánh theo thời gian cố định để chống timing attack
                return CryptographicOperations.FixedTimeEquals(actual, expected);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Kiểm tra xem giá trị đã lưu đã được hash chưa.</summary>
        public static bool IsHashed(string stored) => stored.Contains(':');

        private static byte[] Compute(string password, byte[] salt)
        {
            byte[] pw = Encoding.UTF8.GetBytes(password);
            byte[] combined = new byte[salt.Length + pw.Length];
            Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
            Buffer.BlockCopy(pw, 0, combined, salt.Length, pw.Length);
            return SHA256.HashData(combined);
        }
    }
}
