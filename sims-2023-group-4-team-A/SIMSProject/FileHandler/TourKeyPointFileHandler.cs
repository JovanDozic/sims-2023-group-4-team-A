using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class TourKeyPointFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tourkeypoints.csv";
        private readonly Serializer<TourKeyPoint> _serializer;
        public TourKeyPointFileHandler()
        {
            _serializer = new Serializer<TourKeyPoint>();
        }

        public List<TourKeyPoint> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourKeyPoint> tourkeypoints)
        {
            _serializer.ToCSV(FilePath, tourkeypoints);
        }

    }
}
