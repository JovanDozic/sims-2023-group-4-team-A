#region

using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace DI_Example.Serializer
{
    internal class Serializer2<T> where T : ISerializable, new()
    {
        private const char Delimiter = ';';

        public void ToCSV(string fileName, List<T> objects)
        {
            StringBuilder csv = new();

            foreach (var obj in objects)
            {
                var line = string.Join(Delimiter.ToString(), obj.ToCSV());
                csv.AppendLine(line);
            }

            File.WriteAllText(fileName, csv.ToString());
        }

        public List<T> FromCSV(string fileName)
        {
            List<T> objects = new();

            foreach (var line in File.ReadLines(fileName))
            {
                var csvValues = line.Split(Delimiter);
                T obj = new();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
    }
}