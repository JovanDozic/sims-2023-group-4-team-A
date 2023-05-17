using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    public class AccommodationRenovationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodationRenovations.csv";
        private readonly Serializer<AccommodationRenovation> _serializer;

        public AccommodationRenovationFileHandler()
        {
            _serializer = new Serializer<AccommodationRenovation>();
        }

        public List<AccommodationRenovation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationRenovation> renovations)
        {
            _serializer.ToCSV(FilePath, renovations);
        }
    }
}
