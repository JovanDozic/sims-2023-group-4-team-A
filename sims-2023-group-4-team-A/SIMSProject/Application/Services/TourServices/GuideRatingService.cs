using SIMSProject.Application.DTOs;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.TourServices
{
    public class GuideRatingService
    {
        private readonly IGuideRatingRepo _ratingRepo;
        private readonly ITourReservationRepo _tourReservationRepo;
        private readonly IGuideRepo _guideRepo;

        public GuideRatingService(IGuideRatingRepo ratingRepo, IGuideRepo guideRepo, ITourReservationRepo tourReservationRepo)
        {
            _ratingRepo = ratingRepo;
            _tourReservationRepo = tourReservationRepo;
            _guideRepo = guideRepo;
        }
        public void LeaveRating(GuideRating guideRating, int guideId)
        {
            _ratingRepo.Save(guideRating);
            _tourReservationRepo.Update(guideRating.TourReservation);
            UpdateGuideTotalRating(guideId);
        }

        public Guide GetById(int guideId)
        {
            return _guideRepo.GetById(guideId);
        }
        public void UpdateGuideTotalRating(int guideId)
        {
            var ratings = _ratingRepo.GetAllByGuideId(guideId);
            GetById(guideId).Rating = ratings.Average(x => x.Overall);
            _guideRepo.Update(GetById(guideId));
        }
        public void ReportReview(int id)
        {
            GuideRating review = _ratingRepo.GetById(id);
            review.Reported = true;
            _ratingRepo.SaveAll(_ratingRepo.GetAll());
        }
    }
}
