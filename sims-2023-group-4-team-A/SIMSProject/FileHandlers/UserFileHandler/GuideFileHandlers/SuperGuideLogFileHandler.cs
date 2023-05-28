using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandlers.UserFileHandler.GuideFileHandlers
{
    public class SuperGuideLogFileHandler
    {
        private const string FilePath = "../../../Resources/Data/superguidelogs.csv";
        private readonly Serializer<SuperGuideLog> _serializer;

        public SuperGuideLogFileHandler()
        {
            _serializer = new Serializer<SuperGuideLog>();
        }

        public List<SuperGuideLog> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<SuperGuideLog> users)
        {
            _serializer.ToCSV(FilePath, users);
        }
    }
}
