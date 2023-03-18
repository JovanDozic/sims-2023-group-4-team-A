using SIMSProject.Model.UserModel;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler.UserFileHandler
{
    public class GuestFileHandler
    {
        private const string FilePath = "../../../Resources/Data/Users/guests.csv";
        private readonly Serializer<Guest> _serializer;

        public GuestFileHandler()
        {
            _serializer = new();
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
