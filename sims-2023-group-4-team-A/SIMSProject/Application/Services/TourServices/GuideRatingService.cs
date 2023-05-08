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
        private readonly ITourGuestRepo _tourGuestRepo;
        private readonly ITourAppointmentRepo _tourAppointmentRepo;


        public GuideRatingService(IGuideRatingRepo ratingRepo, IGuideRepo guideRepo, ITourReservationRepo tourReservationRepo, ITourGuestRepo tourGuestRepo, ITourAppointmentRepo tourAppointmentRepo)
        {
            _ratingRepo = ratingRepo;
            _tourReservationRepo = tourReservationRepo;
            _guideRepo = guideRepo;
            _tourGuestRepo = tourGuestRepo;
            _tourAppointmentRepo = tourAppointmentRepo;

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

        public List<TourAppointmentRatingDTO> MapRatingsByTour(int tourId)
        {
            List<TourAppointmentRatingDTO> tourRatings = new();
            List<TourGuest> users = _tourGuestRepo.GetAll();

            tourRatings = _ratingRepo.GetAll()
                .SelectMany(rating => users, (rating, guest) => new { rating, guest })
                .Where(x => x.rating.TourReservation.TourAppointment.Id == x.guest.TourAppointment.Id
                    && x.guest.TourAppointment.Tour.Id == tourId
                    && x.guest.Guest.Id == x.rating.TourReservation.GuestId)
                .Select(x => new TourAppointmentRatingDTO(x.rating, x.guest))
                .ToList();

            return tourRatings;
        }

        public List<DateTime> GetRatedDatesByTourRating(TourRatingDTO tourRating)
        {
            return _ratingRepo.GetRatedDatesByTour(tourRating);
        }

    }
}
