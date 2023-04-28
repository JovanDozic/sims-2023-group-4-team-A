using SIMSProject.Application.DTOs;
using SIMSProject.Application.DTOs.TourDTOs;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

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

        public List<TourAppontmentRatingDTO> MapRatingsByTour(int tourId)
        {
            List<TourAppontmentRatingDTO> tourRatings = new();
            List<TourGuest> users = _tourGuestRepo.GetAll();

            tourRatings = _ratingRepo.GetAll()
                .SelectMany(rating => users, (rating, guest) => new { rating, guest })
                .Where(x => x.rating.TourReservation.TourAppointment.Id == x.guest.TourAppointment.Id
                    && x.guest.TourAppointment.Tour.Id == tourId
                    && x.guest.Guest.Id == x.rating.TourReservation.GuestId)
                .Select(x => new TourAppontmentRatingDTO(x.rating, x.guest))
                .ToList();

            return tourRatings;
        }

        public GuestAgeGroupsDTO DetermineAgeGroups(int tourId)
        {
            var ageGroups = GetCompletedReservationsByTour(tourId)
               .Join(GetPresentGuests(),
               tr => new { KeyOne = tr.TourAppointment.Id, KeyTwo = tr.GuestId },
               tg => new { KeyOne = tg.TourAppointment.Id, KeyTwo = tg.Guest.Id },
               (tr, tg) => new { tg.Guest.Birthday, tg.TourAppointment.Date, tr.GuestNumber })
               .GroupBy(x => 1)
               .Select(group => new GuestAgeGroupsDTO
               {
                   Minors = group.Sum(x => (x.Date - x.Birthday <= TimeSpan.FromDays(365 * 18)) ? x.GuestNumber : 0),
                   Adults = group.Sum(x => (x.Date - x.Birthday > TimeSpan.FromDays(365 * 18) && x.Date - x.Birthday <= TimeSpan.FromDays(365 * 50)) ? x.GuestNumber : 0),
                   Seniors = group.Sum(x => (x.Date - x.Birthday > TimeSpan.FromDays(365 * 50)) ? x.GuestNumber : 0)
               }).FirstOrDefault();

            return ageGroups;
        }
        private IEnumerable<TourReservation> GetCompletedReservationsByTour(int tourId)
        {
            return _tourReservationRepo.GetAll()
                            .Where(tr => tr.TourAppointment != null
                            && tr.TourAppointment.TourStatus == Status.COMPLETED
                            && tr.TourAppointment.Tour.Id == tourId);
        }

        private List<TourGuest> GetPresentGuests()
        {
            return _tourGuestRepo.GetAll().FindAll(x => x.GuestStatus == GuestAttendance.PRESENT);
        }

        public TourStatisticsDTO GetMostFisitedTour(int? targetYear = null)
        {
            var mostVisitedTours = _tourReservationRepo.GetAll()
                .Where(tr => tr.TourAppointment != null
                && tr.TourAppointment.TourStatus == Status.COMPLETED
                && (targetYear == null || tr.TourAppointment.Date.Year == targetYear.Value))
                .Join(GetPresentGuests(),
               tr => new { KeyOne = tr.TourAppointment.Id, KeyTwo = tr.GuestId },
               tg => new { KeyOne = tg.TourAppointment.Id, KeyTwo = tg.Guest.Id },
               (tr, tg) => new { tr.TourAppointment.Tour, tr.GuestNumber, tr.TourAppointment })
                .GroupBy(joined => joined.Tour)
                .Select(group => new TourStatisticsDTO
                {
                    Tour = group.Key,
                    TotalAppointments = group.DistinctBy(x => x.TourAppointment).Count(),
                    TotalGuests = group.Sum(x => x.GuestNumber)
                })
                .OrderByDescending(result => result.TotalGuests);
            return mostVisitedTours.FirstOrDefault();
        }
    }
}
