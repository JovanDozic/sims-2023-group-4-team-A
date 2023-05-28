using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.UserFileHandler
{
    public class Guest1FileHandler
    {
        private const string FilePath = "../../../Resources/Data/Users/guest1.csv";
        private readonly Serializer<Guest1> _serializer;

        public Guest1FileHandler()
        {
            _serializer = new Serializer<Guest1>();
        }

        public List<Guest1> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guest1> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}
