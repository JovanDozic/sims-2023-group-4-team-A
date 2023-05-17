using SIMSProject.Application.DTOs;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace SIMSProject.Application.Services.TourServices
{
    public class TourRatingService
    {
        private readonly ITourRatingRepo _repo;
        private readonly GuideRatingService  _guideRatingService;

        public TourRatingService(ITourRatingRepo repo)
        {
            _repo = repo;
            _guideRatingService = Injector.GetService<GuideRatingService>();
        }

        public void ReportReview(int id)
        {
            _guideRatingService.ReportReview(id);
        }

        public List<DateTime> GetRatedDatesByTourRating(TourRatingDTO tourRating)
        {
            return _repo.GetRatedDatesByTourRating(tourRating);
        }

        public List<TourAppointmentRatingDTO> MapRatingsByTour(int tourId)
        {
            return _repo.MapRatingsByTour(tourId);
        }
    }
}
