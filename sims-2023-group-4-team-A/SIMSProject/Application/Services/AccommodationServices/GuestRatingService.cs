using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class GuestRatingService
    {
        private readonly IGuestRatingRepo _ratingRepo;
        private readonly IGuestRepo _guestRepo;
        private readonly IAccommodationReservationRepo _reservationRepo;

        public GuestRatingService(IGuestRatingRepo ratingRepo, IGuestRepo guestRepo, IAccommodationReservationRepo reservationRepo)
        {
            _ratingRepo = ratingRepo;
            _guestRepo = guestRepo;
            _reservationRepo = reservationRepo;
        }

        public void LeaveRating(GuestRating rating)
        {
            _ratingRepo.Save(rating);
            _reservationRepo.Update(rating.Reservation);
            UpdateGuestsTotalRating(rating.Reservation.Guest);
        }

        public void UpdateGuestsTotalRating(Guest guest)
        {
            var ratings = _ratingRepo.GetAllByGuestId(guest.Id);
            guest.Rating = ratings.Average(x => x.Overall);
            _guestRepo.Update(guest);
        }

        public List<GuestRating> GetAll()
        {
            return _ratingRepo.GetAll();
        }
    }
}
