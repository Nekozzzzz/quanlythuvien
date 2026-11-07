using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement._2.Controls
{
    public static class JsonStorage<T>
    {
        public static List<T> Load(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static void Save(string filePath, List<T> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
