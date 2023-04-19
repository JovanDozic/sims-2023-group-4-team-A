using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface IGuideRatingRepo
    {
        public int NextId();
        public List<GuideRating> GetAll();
        public List<GuideRating> GetAllByGuideId(int guideId);
        public double GetOverallByGuideId(int guideId);
        public GuideRating Save(GuideRating guideRating);
        public void SaveAll(List<GuideRating> guideRatings);
        public GuideRating GetById(int id);
    }
}
