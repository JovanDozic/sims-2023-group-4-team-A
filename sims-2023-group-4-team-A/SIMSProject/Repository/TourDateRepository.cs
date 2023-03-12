using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repository
{
    public class TourDateRepository
    {
        private const string FilePath = "../../../Resources/Data/tourdates.csv";
        private readonly Serializer<TourDate> serializer;

        public TourDateRepository()
        {
            serializer = new Serializer<TourDate>();
        }

        public List<TourDate> Load()
        {
            return serializer.FromCSV(FilePath);
        }

        public void Save(List<TourDate> tourdates)
        {
            serializer.ToCSV(FilePath, tourdates);
        }
    }
}
