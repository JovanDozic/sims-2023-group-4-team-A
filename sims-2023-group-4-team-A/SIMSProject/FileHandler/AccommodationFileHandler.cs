using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class AccommodationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";
        private const string UserImagesFolderPath = "../../../Resources/UserImages/Accommodation_";
        private readonly Serializer<Accommodation> _serializer;

        public AccommodationFileHandler()
        {
            _serializer = new();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(FilePath, accommodations);
        }

        public string SaveImage(string fullSourceFilePath, int accommodationId)
        {
            string fullDestFilePath = UserImagesFolderPath + accommodationId;
            if (!Directory.Exists(fullDestFilePath)) Directory.CreateDirectory(fullDestFilePath);
            fullDestFilePath += "/" + Path.GetFileName(fullSourceFilePath);
            try
            {
                //Trace.WriteLine("source: " + fullSourceFilePath);
                //Trace.WriteLine("dest: " + fullDestFilePath);
                File.Copy(fullSourceFilePath, fullDestFilePath, false);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Failed to save {ex.Message}");
                return string.Empty;
            }
            return fullDestFilePath;
        }
    }
}
