using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
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
            owner.Rating = ratings.Average(x => x.Overall);
            _ownerRepo.Update(owner);
        }
        public Owner GetOwnerById(int ownerId)
        {
            return _ownerRepo.GetById(ownerId);
        }
       
    }
}
