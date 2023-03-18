using SIMSProject.Model;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class GuestRatingFileHandler
    {
        public const string FilePath = "../../../Resources/Data/guestratings.csv";
        private readonly Serializer<GuestRating> _serializer;

        public GuestRatingFileHandler()
        {
            _serializer = new();
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
