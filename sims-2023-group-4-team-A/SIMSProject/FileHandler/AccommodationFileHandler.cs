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
    }
}
