using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SIMSProject.FileHandler
{
    public class TourFileHandler
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";
        private readonly Serializer<Tour> _serializer;
        public TourFileHandler()
        {
            _serializer = new Serializer<Tour>();
        }

        public List<Tour> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Tour> tours)
        {
            _serializer.ToCSV(FilePath, tours);
        }
    }
}
