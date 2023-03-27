using System.Collections.Generic;
using SIMSProject.Model.DAO;
using SIMSProject.Model;

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

        public void SignGuest(int guestId, int tourDateId)
        {
            _tourGuests.SignUpGuest(guestId, tourDateId);
        }

        public void MakeGuestPresent(int guestId, int tourDateId, KeyPoint currentKeyPoint)
        {
            _tourGuests.MakeGuestPresent(guestId, tourDateId, currentKeyPoint);
        }
    }
}
