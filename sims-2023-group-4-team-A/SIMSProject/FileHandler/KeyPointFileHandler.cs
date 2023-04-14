using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class KeyPointFileHandler
    {
        private const string FilePath = "../../../Resources/Data/keypoints.csv";
        private readonly Serializer<KeyPoint> _serializer;

        public KeyPointFileHandler()
        {
            _serializer = new Serializer<KeyPoint>();
        }

        public List<KeyPoint> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<KeyPoint> keyPoints) 
        {
            _serializer.ToCSV(FilePath, keyPoints);
        }
    }
}
