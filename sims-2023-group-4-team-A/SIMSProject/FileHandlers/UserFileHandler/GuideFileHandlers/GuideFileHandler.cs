using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.UserFileHandler
{
    public class GuideFileHandler
    {
        private const string FilePath = "../../../Resources/Data/Users/guides.csv";
        private readonly Serializer<Guide> _serializer;

        public GuideFileHandler()
        {
            _serializer = new Serializer<Guide>();
        }

        public List<Guide> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Guide> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}