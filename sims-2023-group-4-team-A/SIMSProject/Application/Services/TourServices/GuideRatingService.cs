using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
//using SIMSProject.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class GuideRatingService
    {
        private readonly IGuideRatingRepo _ratingRepo;
        private readonly ITourReservationRepo _tourReservationRepo;

        public GuideRatingService(IGuideRatingRepo ratingRepo, ITourReservationRepo tourReservationRepo)
        {
            _ratingRepo = ratingRepo;
            _tourReservationRepo = tourReservationRepo;

        }

        public void UpdateGuideTotalRating(Guide guide)
        {
            
        }
    }
}
