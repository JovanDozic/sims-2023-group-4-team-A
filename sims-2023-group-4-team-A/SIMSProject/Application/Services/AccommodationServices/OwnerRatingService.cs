using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class OwnerRatingService
    {
        private readonly IOwnerRatingRepo _ratingRepo;
        private readonly IOwnerRepo _ownerRepo;
        private readonly IAccommodationReservationRepo _reservationRepo;

        public OwnerRatingService(IOwnerRatingRepo ratingRepo, IOwnerRepo ownerRepo, IAccommodationReservationRepo reservationRepo)
        {
            _ratingRepo = ratingRepo;
            _ownerRepo = ownerRepo;
            _reservationRepo = reservationRepo;
        }

        public OwnerRating GetByReservationId(int reservationId)
        {
            return _ratingRepo.GetByReservationId(reservationId);
        }

        public void LeaveRating(OwnerRating rating)
        {
            _ratingRepo.Save(rating);
            _reservationRepo.Update(rating.Reservation);
            UpdateOwnerTotalRating(rating.Reservation.Accommodation.Owner);
        }

        public void UpdateOwnerTotalRating(Owner owner)
        {
            var ratings = _ratingRepo.GetAllByOwnerId(owner.Id);
            owner.Rating = ratings.Count > 0 ? ratings.Average(x => x.Overall) : 0;
            _ownerRepo.Update(owner);
        }

        public List<OwnerRating> GetAllByOwnerId(int ownerId)
        {
            return _ratingRepo.GetAllByOwnerId(ownerId);
        }

        public List<OwnerRating> GetAllByAccommodationId(int accommodationId)
        {
            return _ratingRepo.GetAllByAccommodationId(accommodationId);
        }

        public int CountAllByOwnerId(int ownerId)
        {
            return _ratingRepo.GetAllByOwnerId(ownerId).Count;
        }

        public User UpdateOwnerInfo(User user)
        {
            if (user is not Owner owner) return null;

            UpdateOwnerTotalRating(owner);
            owner.Role = IsSuperOwner(owner) ? UserRole.SuperOwner : UserRole.Owner;
            _ownerRepo.Update(owner);

            return owner;
        }

        public OwnerRating GetById(int id)
        {
            return _ratingRepo.GetById(id);
        }

        public bool IsSuperOwner(User user)
        {
            if (user is not Owner owner) return false;
            return CountAllByOwnerId(owner.Id) >= Consts.SuperOwnerMinimumRatingCount && owner.Rating >= Consts.SuperOwnerMinimumRating;
        }

        public AccommodationRating CalculateRating(Accommodation accommodation)
        {
            var ratings = _ratingRepo.GetAllByAccommodationId(accommodation.Id);
            try
            {
                return new()
                {
                    CleanlinessRating = ratings.Average(x => x.CleanlinessRating),
                    OwnerCorrectness = ratings.Average(x => x.OwnerCorrectness),
                    Kindness = ratings.Average(x => x.Kindness),
                    NumberOfRatings = ratings.Count
                };
            }
            catch
            {
                return new();
            }
        }

        public void UpdateRatingsForReservations(ObservableCollection<AccommodationReservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                if (reservation.OwnerRated) reservation.OwnerRating = GetByReservationId(reservation.Id).Overall;
            }
        }

        public int CountRenovationSuggestionsByAccommodation(Accommodation accommodation)
        {
            return GetAllByAccommodationId(accommodation.Id).FindAll(x => x.RenovationSuggestion is not null).Count;
        }
    }
}
