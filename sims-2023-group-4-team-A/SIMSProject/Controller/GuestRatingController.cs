using System.Collections.Generic;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class GuestRatingController
    {
        private readonly GuestRatingDAO _guestRatings;
        public GuestRating GuestRating;

        public GuestRatingController()
        {
            _guestRatings = new GuestRatingDAO();
            GuestRating = new GuestRating();
        }

        public void SaveAll(List<GuestRating> ratings)
        {
            _guestRatings.SaveAll(ratings);
        }

        public GuestRating Create(GuestRating rating)
        {
            return _guestRatings.Save(rating);
        }

        public GuestRating GetByID(int id)
        {
            return _guestRatings.Get(id);
        }
    }
}