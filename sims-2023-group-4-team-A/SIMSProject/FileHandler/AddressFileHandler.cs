using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class AddressFileHandler
    {
        private const string FilePath = "../../../Resources/Data/addresses.csv";
        private readonly Serializer<Location> _serializer;

        public AddressFileHandler()
        {
            _serializer = new();
        }

        public List<Location> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Location> addresses)
        {
            _serializer.ToCSV(FilePath, addresses);
        }
    }
}
