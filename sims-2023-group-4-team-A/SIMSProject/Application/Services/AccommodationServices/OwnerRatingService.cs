using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            owner.Rating = ratings.Average(x => x.Overall);
            _ownerRepo.Update(owner);
        }

        public List<OwnerRating> GetAllByOwnerId(int ownerId)
        {
            return _ratingRepo.GetAllByOwnerId(ownerId);
        }

        public int CountAllByOwnerId(int ownerId)
        {
            return _ratingRepo.GetAllByOwnerId(ownerId).Count;
        }

        public User UpdateOwnerInfo(User user)
        {
            if (user is not Owner owner) return null;
            UpdateOwnerTotalRating(owner);

            if (CountAllByOwnerId(user.Id) >= Constants.SuperOwnerMinimumRatings && owner.Rating >= 4.5)
                owner.Role = UserRole.SuperOwner;
            else 
                owner.Role = UserRole.Owner;

            _ownerRepo.Update(owner);

            return owner;
        }
    }
}
