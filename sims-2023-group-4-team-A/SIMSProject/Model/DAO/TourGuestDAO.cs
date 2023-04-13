using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.FileHandler;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO
{
    public class TourGuestDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly TourGuestFileHandler _fileHandler;
        private List<TourGuest> _tourGuests;

        public TourGuestDAO()
        {
            _fileHandler = new TourGuestFileHandler();
            _tourGuests = _fileHandler.Load();
            _observers = new List<IObserver>();

            AssociateTourGuests();
        }

        public List<TourGuest> GetAll()
        {
            return _tourGuests;
        }

        public TourGuest Save(TourGuest tourGuest)
        {
            _tourGuests.Add(tourGuest);
            _fileHandler.Save(_tourGuests);
            NotifyObservers();
            return tourGuest;
        }

        public void SaveAll(List<TourGuest> tourGuests)
        {
            _fileHandler.Save(tourGuests);
            _tourGuests = tourGuests;
            NotifyObservers();
        }

        private void AssociateTourGuests()
        {
            KeyPointFileHandler keyPointFileHandler = new();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();
            TourAppointmentFileHandler dateHandler = new();
            List<TourAppointment> appointments = dateHandler.Load();
            GuestFileHandler guestFileHandler = new();
            List<Guest> tourGuests = guestFileHandler.Load();

            foreach (TourGuest tourGuest in _tourGuests)
            {
                AssociateAppointment(tourGuest, appointments);
                AssociateJoinedKeyPoint(tourGuest, keyPoints);
                AssociateGuest(tourGuests, tourGuest);

            }
        }

        private static void AssociateGuest(List<Guest> tourGuests, TourGuest tourGuest)
        {
            tourGuest.Guest = tourGuests.Find(x => x.Id == tourGuest.GuestId) ?? throw new System.Exception("Error!No matching guest!");
        }

        private static void AssociateJoinedKeyPoint(TourGuest tourGuest, List<KeyPoint> keyPoints)
        {
            if (tourGuest.JoinedKeyPointId == -1)
                return;

            tourGuest.JoinedKeyPoint = keyPoints.Find(x => x.Id == tourGuest.JoinedKeyPointId) ?? throw new System.Exception("Error!No matching keyPoint!");
        }

        private static void AssociateAppointment(TourGuest tourGuest, List<TourAppointment> appointments)
        {
            tourGuest.Appointment = appointments.Find(x => x.Id == tourGuest.AppointmentId) ?? throw new System.Exception("Error!No matching appointment!");
        }

        public void SignUpGuest(int guestId, int tourAppointmentId)
        {
            TourGuest? tourGuest = _tourGuests.Find(x => x.GuestId ==  guestId && x.AppointmentId == tourAppointmentId);
            if(tourGuest == null) return;

            tourGuest.GuestStatus = "Prijavljen";
            SaveAll(_tourGuests);
        }


        public void MakeGuestPresent(int guestId, int tourAppointmentId, KeyPoint currentKeyPoint)
        {
            TourGuest? tourGuest = _tourGuests.Find(x => x.GuestId == guestId && x.AppointmentId == tourAppointmentId);
            if (tourGuest == null) return;

            tourGuest.JoinedKeyPoint = currentKeyPoint;
            tourGuest.JoinedKeyPointId = currentKeyPoint.Id;
        }

        public List<TourGuest> GetGuestsIds(int tourAppointmentId)
        {
            return _tourGuests.FindAll(x => x.AppointmentId == tourAppointmentId);
        }


        // [OBSERVERS]
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}