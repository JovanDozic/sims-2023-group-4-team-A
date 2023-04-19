using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.UserFileHandler
{
    public class OwnerFileHandler
    {
        private const string FilePath = "../../../Resources/Data/Users/owners.csv";
        private readonly Serializer<Owner> _serializer;

        public OwnerFileHandler()
        {
            _serializer = new Serializer<Owner>();
        }

        public List<Owner> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Owner> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}