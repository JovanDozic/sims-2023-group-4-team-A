using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandlers.AccommodationFileHandlers
{
    public class OwnerRatingFileHandler
    {
        public const string FilePath = "../../../Resources/Data/ownerratings.csv";
        private readonly Serializer<OwnerRating> _serializer;

        public OwnerRatingFileHandler()
        {
            _serializer = new Serializer<OwnerRating>();
        }

        public List<OwnerRating> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<OwnerRating> ownerRating)
        {
            _serializer.ToCSV(FilePath, ownerRating);
        }
    }
}
