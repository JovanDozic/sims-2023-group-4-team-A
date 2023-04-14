using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IOwnerRatingRepo
    {
        public void Load();
        public OwnerRating GetById(int ratingId);
        public OwnerRating GetByReservationId(int reservationId);
        public List<OwnerRating> GetAllByOwnerId(int ownerId);
        public List<OwnerRating> GetAll();
        public double GetOverallByOwnerId(int ownerId);
        public int NextId();
        public OwnerRating Save(OwnerRating rating);
        public void SaveAll(List<OwnerRating> ratings);
    }
}
