using BookManagement._3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement._5.Services
{
    public class ReaderService
    {
        private readonly BookManagementDbContext _db;
        public ReaderService(BookManagementDbContext db) => _db = db;

        public List<Reader> GetAll() => _db.Readers.ToList();
        public Reader? GetById(int id) => _db.Readers.FirstOrDefault(r => r.Id == id);

        public List<Reader> Search(string keyword)
        {
            keyword = keyword.ToLower();
            return _db.Readers.Where(r =>
                r.Name.ToLower().Contains(keyword) ||
                (r.Email != null && r.Email.ToLower().Contains(keyword)) ||
                (r.Phone != null && r.Phone.Contains(keyword))).ToList();
        }

        public void Add(Reader reader) { _db.Readers.Add(reader); _db.SaveChanges(); }

        public void Update(int id, Reader updated)
        {
            var r = _db.Readers.FirstOrDefault(x => x.Id == id)
                ?? throw new InvalidOperationException("Khong tim thay doc gia.");
            r.Name = updated.Name; r.Email = updated.Email; r.Phone = updated.Phone;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var r = _db.Readers.FirstOrDefault(x => x.Id == id)
                ?? throw new InvalidOperationException("Khong tim thay doc gia.");
            _db.Readers.Remove(r); _db.SaveChanges();
        }
    }
}
