using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.UserFileHandler
{
    public class Guest2FileHandler
    {
        private const string FilePath = "../../../Resources/Data/Users/guest2.csv";
        private readonly Serializer<Guest2> _serializer;

        public Guest2FileHandler()
        {
            _serializer = new Serializer<Guest2>();
        }

        public List<Guest2> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guest2> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}