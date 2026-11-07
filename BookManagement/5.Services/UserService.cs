using BookManagement._3.Models;
using BookManagement._4.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement._5.Services
{
    public class UserService
    {
        private readonly BookManagementDbContext _db;
        public UserService(BookManagementDbContext db) => _db = db;

        public List<User> GetAll() => _db.Users.ToList();
        public User? GetById(int id) => _db.Users.FirstOrDefault(u => u.Id == id);
        public bool UsernameExists(string username) => _db.Users.Any(u => u.Username == username);

        public User? Authenticate(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !PasswordHasher.Verify(password, user.Password)) return null;
            return user;
        }

        public void Add(User user)
        {
            user.Password = PasswordHasher.Hash(user.Password);
            _db.Users.Add(user); _db.SaveChanges();
        }

        public void Update(int id, User updated, string? newPlainPassword = null)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id)
                ?? throw new InvalidOperationException("Khong tim thay nguoi dung.");
            user.FullName = updated.FullName; user.Role = updated.Role;
            user.Email = updated.Email; user.Phone = updated.Phone;
            user.IsActive = updated.IsActive;
            if (!string.IsNullOrEmpty(newPlainPassword))
                user.Password = PasswordHasher.Hash(newPlainPassword);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id)
                ?? throw new InvalidOperationException("Khong tim thay nguoi dung.");
            _db.Users.Remove(user); _db.SaveChanges();
        }

        public string ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId)
                ?? throw new InvalidOperationException("Khong tim thay nguoi dung.");
            if (!PasswordHasher.Verify(oldPassword, user.Password))
                throw new UnauthorizedAccessException("Mat khau hien tai khong chinh xac.");
            user.Password = PasswordHasher.Hash(newPassword);
            _db.SaveChanges();
            return user.Password;
        }
    }
}
