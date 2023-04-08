using SIMSProject.Domain.Models.TourModels;
using SIMSProject.FileHandler.CSVManager;
using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class KeyPointFileHandler: CSVManager<KeyPoint>
    {
        private const string FilePath = "../../../Resources/Data/keypoints.csv";

        public KeyPointFileHandler(): base(FilePath)
        {
        }

        public List<KeyPoint> Load()
        {
            return FromCSV();
        }

        public void Save(List<KeyPoint> keyPoints) 
        {
            ToCSV(keyPoints);
        }

        protected override KeyPoint ParseItemFromCSV(string[] values)
        {
            KeyPoint keyPoint = new KeyPoint();
            keyPoint.Id = Convert.ToInt32(values[0]);
            keyPoint.Description = values[1];
            keyPoint.LocationId = Convert.ToInt32(values[2]);
            return keyPoint;
        }

        protected override string[] ParseItemToCsv(KeyPoint keyPoint)
        {
            string[] csvValues = { keyPoint.Id.ToString(), keyPoint.Description, keyPoint.LocationId.ToString() };
            return csvValues;
        }
    }
}
