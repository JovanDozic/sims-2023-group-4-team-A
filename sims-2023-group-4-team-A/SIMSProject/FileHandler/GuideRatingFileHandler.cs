using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.FileHandler
{
    public class GuideRatingFileHandler
    {
        public const string FilePath = "../../../Resources/Data/guideratings.csv";
        private readonly Serializer<GuideRating> _serializer;

        public GuideRatingFileHandler()
        {
            _serializer = new Serializer<GuideRating>();
        }

        public List<GuideRating> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<GuideRating> guideRatings)
        {
            _serializer.ToCSV(FilePath, guideRatings);
        }
    }
}
