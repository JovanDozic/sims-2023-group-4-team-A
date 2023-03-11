using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repository
{
    public class TourKeyPointRepository
    {
        private const string FilePath = "../../../Resources/Data/tourkeypoints.csv";
        private readonly Serializer<TourKeyPoint> serializer;

        public TourKeyPointRepository()
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
