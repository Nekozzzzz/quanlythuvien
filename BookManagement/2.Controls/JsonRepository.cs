using BookManagement._3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement._2.Controls
{
    public class JsonRepository<T> where T : IEntity
    {
        private readonly string _filePath;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<T> Load()
        {
            return JsonStorage<T>.Load(_filePath);
        }

        public void Save(List<T> items)
        {
            JsonStorage<T>.Save(_filePath, items);
        }

        public void Add(T item)
        {
            var items = Load();
            int newId = items.Any() ? items.Max(x => x.Id) + 1 : 1;
            item.Id = newId;
            items.Add(item);
            Save(items);
        }

        public void Update(T item)
        {
            var items = Load();
            var index = items.FindIndex(x => x.Id == item.Id);

            if (index >= 0)
            {
                items[index] = item;
                Save(items);
            }
            else
            {
                throw new Exception("Không tìm thấy bản ghi để cập nhật.");
            }
        }

        public List<T> Find(string keyword)
        {
            var items = Load();

            var property = typeof(T).GetProperty("Title") ?? typeof(T).GetProperty("Name");
             
            if (property == null || property.PropertyType != typeof(string))
                throw new Exception("Không tìm thấy thuộc tính 'Title' hoặc 'Name' kiểu string để tìm kiếm.");

            var listFind = items.Where(x =>
            {
                var value = property.GetValue(x) as string;
                return !string.IsNullOrEmpty(value) && value.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            }).ToList();

            return listFind;
        }

        public void Delete(int id)
        {
            var items = Load();
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                items.Remove(item);
                Save(items);
            }
            else
            {
                throw new Exception("Không tìm thấy bản ghi để xóa.");
            }
        }

        public T GetItemFromId(int id)
        {
            var items = Load();
            var item = items.FirstOrDefault(x=> x.Id == id);

            return item;
        }
    }
}
