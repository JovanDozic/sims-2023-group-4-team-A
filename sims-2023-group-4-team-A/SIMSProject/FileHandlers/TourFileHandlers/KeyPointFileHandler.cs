using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandlers.TourFileHandlers
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
