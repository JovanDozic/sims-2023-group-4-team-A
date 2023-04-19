using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.UserFileHandler
{
    public class GuestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/Users/guests.csv";
        private readonly Serializer<Guest> _serializer;

        public GuestFileHandler()
        {
            _serializer = new Serializer<Guest>();
        }

        public List<Guest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guest> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}