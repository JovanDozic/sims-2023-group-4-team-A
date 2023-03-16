using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.FileHandler
{
    public class LocationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";
        private readonly Serializer<Location> serializer;

        public LocationFileHandler()
        {
            serializer = new Serializer<Location>();
        }

        public List<Location> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<Location> tourlocations)
        {
            serializer.ToCSV(FilePath, tourlocations);
        }
    }
}
