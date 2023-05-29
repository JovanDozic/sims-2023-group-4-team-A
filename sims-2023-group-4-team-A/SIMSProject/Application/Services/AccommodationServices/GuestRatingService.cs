using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class GuestRatingService
    {
        private readonly IGuestRatingRepo _ratingRepo;
        private readonly IGuest1Repo _guestRepo;
        private readonly IAccommodationReservationRepo _reservationRepo;

        public GuestRatingService(IGuestRatingRepo ratingRepo, IGuest1Repo guestRepo, IAccommodationReservationRepo reservationRepo)
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

        public void UpdateGuestsTotalRating(Guest1 guest)
        {
            var ratings = _ratingRepo.GetAllByGuestId(guest.Id);
            guest.Rating = ratings.Average(x => x.Overall);
            _guestRepo.Update(guest);
        }

        public GuestRating GetByReservationId(int reservationId)
        {
            return _ratingRepo.GetByReservationId(reservationId);
        }

        public List<GuestRating> GetAll()
        {
            return _ratingRepo.GetAll();
        }

        public void UpdateRatingsForReservations(ObservableCollection<AccommodationReservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                if (reservation.GuestRated) reservation.GuestRating = GetByReservationId(reservation.Id).Overall;
            }
        }
    }
}
