using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;

namespace SIMSProject.Repository
{
    public class AddressRepository
    {
        private const string FilePath = "../../../Resources/Data/addresses.csv";
        private readonly Serializer<Location> _serializer;

        public AddressRepository()
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
