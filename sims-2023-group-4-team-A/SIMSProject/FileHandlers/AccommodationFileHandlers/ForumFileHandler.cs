using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    public class ForumFileHandler
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";
        private readonly Serializer<Forum> _serializer;

        public ForumFileHandler()
        {
            _serializer = new Serializer<Forum>();
        }

        public List<Forum> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Forum> forums)
        {
            _serializer.ToCSV(FilePath, forums);
        }
    }
}
