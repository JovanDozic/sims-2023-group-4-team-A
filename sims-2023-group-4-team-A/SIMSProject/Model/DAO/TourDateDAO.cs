using System;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.FileHandler;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO
{
    public class TourDateDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly TourDateFileHandler _fileHandler;
        private List<TourDate> _tourDates;

        public TourDateDAO()
        {
            _fileHandler = new TourDateFileHandler();
            _tourDates = _fileHandler.Load();
            _observers = new List<IObserver>();

            AssociateTourDates();
        }

        public int NextId()
        {
            try
            {
                return _tourDates.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<TourDate> GetAll()
        {
            return _tourDates;
        }

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
            foreach (var date in _tourDates)
            {
                AssociateTour(date);
                AssociateCurrenKeyPoint(date);
                AssociateGuests(date);
            }
        }

        private static void AssociateGuests(TourDate date)
        {
            GuestFileHandler guestFileHandler = new();
            var guests = guestFileHandler.Load();
            TourGuestFileHandler tourGuestFileHandler = new();
            var tourGuests = tourGuestFileHandler.Load();


            var pairs = tourGuests.FindAll(x => x.TourDateId == date.Id);
            foreach (var pair in pairs)
            {
                var matchingGuest = guests.Find(x => x.Id == pair.GuestId);
                if (matchingGuest == null)
                {
                    continue;
                }

                date.Guests.Add(matchingGuest);
            }
        }

        private static void AssociateCurrenKeyPoint(TourDate date)
        {
            KeyPointFileHandler keyPointFileHandler = new();
            var keyPoints = keyPointFileHandler.Load();

            var current = keyPoints.Find(x => x.Id == date.CurrentKeyPointId);
            if (current == null)
            {
                return;
            }

            date.CurrentKeyPoint = current;
        }

        private static void AssociateTour(TourDate date)
        {
            TourFileHandler tourFileHandler = new();
            var tours = tourFileHandler.Load();

            var tour = tours.Find(x => x.Id == date.TourId);
            if (tour == null)
            {
                return;
            }

            date.Tour = tour;
        }

        public List<TourDate> GetAllByTourId(int tourId)
        {
            List<TourDate> dates = new();
            foreach (var date in GetAll())
            {
                if (date.TourId == tourId)
                {
                    dates.Add(date);
                }
            }

            return dates;
        }

        public void AdvanceToNextKeyPoint(int dateId, KeyPoint nextKeyPoint)
        {
            var date = Get(dateId);
            if (date == null)
            {
                return;
            }

            date.CurrentKeyPoint = nextKeyPoint;
            date.CurrentKeyPointId = nextKeyPoint.Id;
            SaveAll(_tourDates);
        }

        public void UpdateAvailableSpots(TourDate tourDate)
        {
            var oldTourDate = _tourDates.Find(x => x.Id == tourDate.Id);
            if (oldTourDate == null)
            {
                return;
            }

            oldTourDate.AvailableSpots = tourDate.AvailableSpots;
            _fileHandler.Save(_tourDates);
            NotifyObservers();
        }

        public void StartLiveTracking(TourDate tourDate)
        {
            var oldTourDate = Get(tourDate.Id);
            if (oldTourDate == null)
            {
                return;
            }

            oldTourDate.TourStatus = "Aktivna";
            oldTourDate.CurrentKeyPoint = tourDate.CurrentKeyPoint;
            oldTourDate.CurrentKeyPointId = tourDate.CurrentKeyPointId;
            _fileHandler.Save(_tourDates);
        }

        public void EndTourDate(int dateId)
        {
            var toEnd = Get(dateId);
            if (toEnd == null)
            {
                return;
            }

            toEnd.TourStatus = "Završena";
            _fileHandler.Save(_tourDates);
        }

        public TourDate InitializeTour(TourDate tourDate, Tour tour)
        {
            var oldDate = _tourDates.Find(x => x.Id == tourDate.Id);
            if (oldDate == null)
            {
                return null;
            }

            oldDate.Tour = tour;
            oldDate.TourId = tour.Id;
            oldDate.AvailableSpots = tour.MaxGuestNumber;
            _fileHandler.Save(_tourDates);
            return oldDate;
        }

        public List<TourDate> FindTodaysDates(int TourId)
        {
            return _tourDates.FindAll(x => x.TourId == TourId && x.Date.Date == DateTime.Now.Date);
        }

        public List<TourDate> FindByTour(int TourId)
        {
            return _tourDates.FindAll(x => x.TourId == TourId);
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