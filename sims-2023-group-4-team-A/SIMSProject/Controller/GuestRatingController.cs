using SIMSProject.Model;
using SIMSProject.Model.DAO;
using System.Collections.Generic;

namespace SIMSProject.Controller
{
    public class GuestRatingController
    {
        private GuestRatingDAO _guestRatings;
        public GuestRating GuestRating;

        public GuestRatingController()
        {
            _guestRatings = new();
            GuestRating = new();
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
