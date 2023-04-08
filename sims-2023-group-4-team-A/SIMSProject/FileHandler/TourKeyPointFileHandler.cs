using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class TourKeyPointFileHandler: CSVManager<TourKeyPoint>
    {
        private const string FilePath = "../../../Resources/Data/tourkeypoints.csv";

        public TourKeyPointFileHandler(): base(FilePath)
        {
        }

        public List<TourKeyPoint> Load()
        {
            return FromCSV();
        }

        public void Save(List<TourKeyPoint> tourkeypoints)
        {
            ToCSV(tourkeypoints);
        }

        protected override TourKeyPoint ParseItemFromCSV(string[] values)
        {
            TourKeyPoint tourKeyPoint = new();
            tourKeyPoint.TourId = Convert.ToInt32(values[0]);
            tourKeyPoint.KeyPointId = Convert.ToInt32(values[1]);
            return tourKeyPoint;
        }

        protected override string[] ParseItemToCsv(TourKeyPoint tourKeyPoint)
        {
            string[] csvValues = { tourKeyPoint.TourId.ToString(), tourKeyPoint.KeyPointId.ToString() };
            return csvValues;
        }
    }
}
