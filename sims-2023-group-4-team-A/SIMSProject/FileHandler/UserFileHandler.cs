using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;
namespace SIMSProject.FileHandler
{
    public class UserFileHandler
    {
        private const string FilePath = "../../../Resources/Data/users.csv";
        private readonly Serializer<User> _serializer;

        public UserFileHandler()
        {
            _serializer = new();
        }

        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<User> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}
