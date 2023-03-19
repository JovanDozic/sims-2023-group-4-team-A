using System.Collections.Generic;
using System.Linq;
using SIMSProject.Observer;
using SIMSProject.FileHandler;
using SIMSProject.Controller;
using System.Windows.Navigation;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Model.UserModel;

namespace SIMSProject.Model.DAO
{
    public class TourDateDAO : ISubject
    {
        private List<IObserver> _observers;
        private readonly TourDateFileHandler _fileHandler;
        private List<TourDate> _tourDates;

        public TourDateDAO()
        {
            _fileHandler = new();
            _tourDates = _fileHandler.Load();
            _observers = new();

            AssociateTourDates();

        }

        public int NextId() { return _tourDates.Max(x => x.Id) + 1; }
        public List<TourDate> GetAll() { return _tourDates; }
        public TourDate Get(int id)
        {
            return _tourDates.Find(x => x.Id == id);
        }

        public TourDate Save(TourDate tourDate)
        {
            tourDate.Id = NextId();
            _tourDates.Add(tourDate);
            _fileHandler.Save(_tourDates);
            NotifyObservers();
            return tourDate;
        }

        public void SaveAll(List<TourDate> tourDates)
        {
            _fileHandler.Save(tourDates);
            _tourDates = tourDates;
            NotifyObservers();
        }



        private void AssociateTourDates()
        {


            foreach (TourDate date in _tourDates)
            {
                AssociateTour(date);
                AssociateCurrenKeyPoint(date);
                AssociateGuests(date);
            }
        }

        private static void AssociateGuests(TourDate date)
        {
            GuestFileHandler guestFileHandler = new();
            List<Guest> guests = guestFileHandler.Load();
            TourGuestFileHandler tourGuestFileHandler = new();
            List<TourGuest> tourGuests = tourGuestFileHandler.Load();


            List<TourGuest> pairs = tourGuests.FindAll(x => x.TourDateId == date.Id);
            foreach (var pair in pairs)
            {
                Guest? matchingGuest = guests.Find(x => x.Id == pair.GuestId);
                if (matchingGuest == null) continue;
                date.Guests.Add(matchingGuest);
            }
        }

        private static void AssociateCurrenKeyPoint(TourDate date)
        {
            KeyPointFileHandler keyPointFileHandler = new();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();

            KeyPoint? current = keyPoints.Find(x => x.Id == date.CurrentKeyPointId);
            if (current == null) return;
            date.CurrentKeyPoint = current;
        }

        private static void AssociateTour(TourDate date)
        {
            TourFileHandler tourFileHandler = new();
            List<Tour> tours = tourFileHandler.Load();

            Tour? tour = tours.Find(x => x.Id == date.TourId);
            if (tour == null) return;
            date.Tour = tour;
        }

        public List<TourDate> FindByTour(int TourId)
        {
            return _tourDates.FindAll(x => x.TourId == TourId);
        }

        public void AdvanceToNextKeyPoint(int dateId, KeyPoint nextKeyPoint)
        {
            TourDate? date = Get(dateId);
            if (date == null) return;

            date.CurrentKeyPoint = nextKeyPoint;
            date.CurrentKeyPointId = nextKeyPoint.Id;
            _fileHandler.Save(_tourDates);
        }

        public void StartLiveTracking(TourDate tourDate)
        {
            TourDate? oldTourDate = Get(tourDate.Id);
            if (oldTourDate == null) return;

            oldTourDate.TourStatus = "Aktivna";
            oldTourDate.CurrentKeyPoint = tourDate.CurrentKeyPoint;
            oldTourDate.CurrentKeyPointId = tourDate.CurrentKeyPointId;
            _fileHandler.Save(_tourDates);
        }

        public void EndTourDate(int dateId)
        {
            TourDate? toEnd = Get(dateId);
            if (toEnd == null) return;
            toEnd.TourStatus = "Završena";
            _fileHandler.Save(_tourDates);
        }

        public TourDate InitializeTour(TourDate tourDate, Tour tour)
        {
            TourDate? oldDate = _tourDates.Find(x => x.Id ==  tourDate.Id);
            if(oldDate == null) return null;

            oldDate.Tour = tour;
            oldDate.TourId = tour.Id;
            oldDate.AvailableSpots = tour.MaxGuestNumber;
            _fileHandler.Save(_tourDates);
            return oldDate;
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
