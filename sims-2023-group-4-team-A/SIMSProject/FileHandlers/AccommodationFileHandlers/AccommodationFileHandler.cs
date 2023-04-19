using System.Collections.Generic;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    public class AccommodationFileHandler
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";
        private readonly Serializer<Accommodation> _serializer;

        public AccommodationFileHandler()
        {
            _serializer = new Serializer<Accommodation>();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(FilePath, accommodations);
        }
    }
}