using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repository
{
    public class LocationRepository
    {
        private const string FilePath = "../../../Resources/Data/locations1.csv";
        private readonly Serializer<Location> serializer;

        public LocationRepository()
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
