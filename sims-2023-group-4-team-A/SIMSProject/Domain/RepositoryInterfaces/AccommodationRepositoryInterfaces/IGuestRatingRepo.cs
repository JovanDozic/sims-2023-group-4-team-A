using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces
{
    public interface IGuestRatingRepo
    {
        public void Load();
        public GuestRating GetById(int ratingId);
        public List<GuestRating> GetByGuestId(int guestId);
        public List<GuestRating> GetAll();
        public int NextId();
        public GuestRating Save(GuestRating rating);
        public void SaveAll(List<GuestRating> ratings);
    }
}
