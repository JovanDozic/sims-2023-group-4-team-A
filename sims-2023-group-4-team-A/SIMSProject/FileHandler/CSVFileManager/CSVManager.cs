using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler.CSVManager
{
    public abstract class CSVManager<T>
    {
        private readonly string _filePath;
        private readonly char _delimiter = '|';

        public CSVManager(string filePath)
        {
            _filePath = filePath;
        }

        protected abstract string[] ParseItemToCsv(T item);
        protected abstract T ParseItemFromCSV(string[] values);

        protected void ToCSV(List<T> items)
        {
            StringBuilder csv = new();

            foreach (var item in items)
            {
                var line = string.Join(_delimiter.ToString(), ParseItemToCsv(item));
                csv.AppendLine(line);
            }

            File.WriteAllText(_filePath, csv.ToString());
        }

        protected List<T> FromCSV()
        {
            List<T> items = new();

            foreach (var line in File.ReadLines(_filePath))
            {
                var csvValues = line.Split(_delimiter);
                T obj = ParseItemFromCSV(csvValues);
                items.Add(obj);
            }
            return items;
        }
    }
}
