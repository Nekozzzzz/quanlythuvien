using System;

namespace BookManagement._3.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // "Admin" hoặc "User"
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
