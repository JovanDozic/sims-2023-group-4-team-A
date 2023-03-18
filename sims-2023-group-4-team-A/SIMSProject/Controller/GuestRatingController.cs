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
        public void SaveAll(List<GuestRating> locations)
        {
            _guestRatings.SaveAll(locations);
        }

        public GuestRating Create(GuestRating location)
        {
            return _guestRatings.Save(location);
        }

        public GuestRating GetByID(int id)
        {
            return _guestRatings.Get(id);
        }
    }
}
