using System.Collections.Generic;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;

namespace SIMSProject.FileHandler
{
    public class GuestRatingFileHandler
    {
        public const string FilePath = "../../../Resources/Data/guestratings.csv";
        private readonly Serializer<GuestRating> _serializer;

        public GuestRatingFileHandler()
        {
            _serializer = new Serializer<GuestRating>();
        }

        public List<GuestRating> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<GuestRating> guestRatings)
        {
            _serializer.ToCSV(FilePath, guestRatings);
        }
    }
}