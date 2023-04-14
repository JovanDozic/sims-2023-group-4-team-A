using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class TourKeyPointFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourkeypoints.csv";
        private readonly Serializer<TourKeyPoint> serializer;

        public TourKeyPointFileHandler()
        {
            serializer = new Serializer<TourKeyPoint>();
        }

        public List<TourKeyPoint> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<TourKeyPoint> tourkeypoints)
        {
            serializer.ToCSV(FilePath, tourkeypoints);
        }
    }
}
