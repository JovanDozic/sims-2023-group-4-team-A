using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
{
    public interface IGuideRatingRepo
    {
        public int NextId();
        public List<GuideRating> GetAll();
        public List<GuideRating> GetByGuideId(int guideId);
        public GuideRating Save(GuideRating guideRating);
        public void SaveAll(List<GuideRating> guideRatings);
        public GuideRating GetById(int id);
    }
}
