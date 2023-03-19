using SIMSProject.Observer;
using SIMSProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace SIMSProject.Model.DAO
{
    public class TourDAO : ISubject
    {
        private List<IObserver> _observers;
        private readonly TourFileHandler _fileHandler;
        private List<Tour> _tours;

        public TourDAO()
        {
            _fileHandler = new();
            _tours = _fileHandler.Load();
            _observers = new();

            AssociateTours();
        }

        public int NextId() { return _tours.Max(x => x.Id) + 1; }
        public List<Tour> GetAll() { return _tours; }

        public Tour Get(int id)
        {
            return _tours.Find(x => x.Id == id);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours.Add(tour);
            _fileHandler.Save(_tours);
            NotifyObservers();
            return tour;
        }

        public void SaveAll(List<Tour> tours)
        {
            _fileHandler.Save(tours);
            _tours = tours;
            NotifyObservers();
        }

        private void AssociateTours()
        {
            foreach (var tour in _tours)
            {
                AssociateLocation(tour);
                AssociateDates(tour);
                AssociateKeyPoints(tour);

            }
        }

        private static void AssociateLocation(Tour tour)
        {
            LocationFileHandler tourLocationFileHandler = new();
            List<Location> tourLocations = tourLocationFileHandler.Load();

            Location? matchingLocation = tourLocations.Find(x => x.Id == tour.LocationId);
            if (matchingLocation == null) return;
            tour.Location = matchingLocation;
        }

        private static void AssociateDates(Tour tour)
        {
            TourDateFileHandler tourDateFileHandler = new();
            List<TourDate> tourDates = tourDateFileHandler.Load();

            tour.Dates.AddRange(tourDates.FindAll(x => x.TourId == tour.Id));
        }

        private static void AssociateKeyPoints(Tour tour)
        {
            TourKeyPointFileHandler tourKeyPointFileHandler = new();
            KeyPointFileHandler keyPointFileHandler = new();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointFileHandler.Load();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();

            List<TourKeyPoint> pairs = tourKeyPoints.FindAll(x => x.TourId == tour.Id);
            foreach (var pair in pairs)
            {
                KeyPoint? matchingKeyPoint = keyPoints.Find(x => x.Id == pair.KeyPointId);
                if (matchingKeyPoint == null) continue;
                tour.KeyPoints.Add(matchingKeyPoint);
            }
        }


        public List<Tour> SearchLocations(string locationId)
        {
            return _tours.Where(tour => tour.LocationId.Equals(locationId)).ToList();
        }

        public List<Tour> SearchDurations(string duration)
        {
            return _tours.Where(tour => tour.Duration.Equals(duration)).ToList();
        }

        public List<Tour> SearchLanguages(string language)
        {
            return _tours.Where(tour => tour.TourLanguage.Equals(language)).ToList();
        }

        public List<Tour> SearchMaxGuests(string maxGuests)
        {
            return _tours.Where(tour => tour.MaxGuestNumber.Equals(maxGuests)).ToList();
        }

        public List<Tour> FindTodaysTours()
        {
            return _tours.FindAll(x => x.Dates.Any(x => x.Date.Date == DateTime.Today.Date));
        }

        public KeyPoint GetNextKeyPoint(TourDate date)
        {
            Tour? currentTour = Get(date.TourId);
            if (currentTour == null) return null;

            int currentIndex = currentTour.KeyPoints.FindIndex(x => x.Id == date.CurrentKeyPointId);
            bool indexOutOfRange = currentIndex < 0 || currentIndex >= currentTour.KeyPoints.Count - 1;

            if (indexOutOfRange) return null;

            return currentTour.KeyPoints[currentIndex + 1];
        }

        public KeyPoint FindLastKeyPoint(TourDate date)
        {
            Tour? currentTour = Get(date.TourId);
            if (currentTour == null) return null;
            return currentTour.KeyPoints.Last();
        }

        public void EndTour(int tourId, int dateId)
        {
            Tour? toEnd = Get(tourId);
            if (toEnd == null) return;

            TourDate? dateToEnd = toEnd.Dates.Find(x => x.Id == dateId);
            if (dateToEnd == null) return;
            dateToEnd.TourStatus = "Završena";
            _fileHandler.Save(_tours);
        }


        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

    }
}
