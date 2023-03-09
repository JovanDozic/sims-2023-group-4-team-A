using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repository
{
    public class AddressRepository
    {
        private const string FilePath = "../../../Resources/Data/addresses.csv";
        private readonly Serializer<Address> _serializer;

        public AddressRepository()
        {
            _serializer = new();
        }

        public List<Address> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Address> addresses)
        {
            _serializer.ToCSV(FilePath, addresses);
        }
    }
}
