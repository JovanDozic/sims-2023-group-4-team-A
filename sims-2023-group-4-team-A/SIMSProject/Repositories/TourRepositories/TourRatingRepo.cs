using SIMSProject.Application.DTOs;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourRatingRepo : ITourRatingRepo
    {
        private readonly IGuideRatingRepo _ratingRepo;
        private readonly ITourGuestRepo _tourGuestRepo;

        public TourRatingRepo(IGuideRatingRepo guideRatingRepo, ITourGuestRepo tourGuestRepo)
        {
            _ratingRepo = guideRatingRepo;
            _tourGuestRepo = tourGuestRepo;
        }

        public List<DateTime> GetRatedDatesByTourRating(TourRatingDTO tourRating)
        {
            return tourRating.Ratings.Select(x => x.User.TourAppointment.Date).Distinct().ToList();
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
    }
}
