using System.Collections.Generic;
using SIMSProject.Model.DAO;
using SIMSProject.Model;
using SIMSProject.Observer;

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

        public void SignGuest(int guestId, int tourAppointmentId)
        {
            _tourGuests.SignUpGuest(guestId, tourAppointmentId);
        }

        public void MakeGuestPresent(int guestId, int tourAppointmentId, KeyPoint currentKeyPoint)
        {
            _tourGuests.MakeGuestPresent(guestId, tourAppointmentId, currentKeyPoint);
        }

        public List<TourGuest> GetGuestsIds(int tourAppointmentId)
        {
            return _tourGuests.GetGuestsIds(tourAppointmentId);
        }

        public void Subscribe(IObserver observer)
        {
            _tourGuests.Subscribe(observer);
        }
    }
}
