using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class TourGuestController
    {
        private TourGuestDAO _tourGuests;
        public TourGuest TourGuest;

        public TourGuestController()
        {
            _tourGuests = new();
            TourGuest = new();
        }

        public List<TourGuest> GetAll()
        {
            return _tourGuests.GetAll();
        }

        public void SaveAll(List<TourGuest> tourGuests)
        {
            _tourGuests.SaveAll(tourGuests);
        }

        public TourGuest Create(TourGuest tourGuest)
        {
            return _tourGuests.Save(tourGuest);
        }
    }
}
