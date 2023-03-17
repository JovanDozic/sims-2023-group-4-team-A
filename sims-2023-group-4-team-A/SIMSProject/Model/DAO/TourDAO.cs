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

        private void AssociateTours()
        {
            LocationFileHandler tourLocationFileHandler = new();
            TourKeyPointFileHandler tourKeyPointFileHandler = new();
            KeyPointFileHandler keyPointFileHandler = new();
            TourDateFileHandler tourDateFileHandler = new();

            List<Location> tourLocations = tourLocationFileHandler.Load();
            List<TourDate> tourDates = tourDateFileHandler.Load();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointFileHandler.Load();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();


            foreach (var tour in _tours)
            {
                AssociateLocation(tourLocations, tour);
                AssociateDates(tourDates, tour);
                AssociateKeyPoints(tourKeyPoints, keyPoints, tour);

            }
        }

        private static void AssociateLocation(List<Location> tourLocations, Tour tour)
        {
            tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
        }

        private static void AssociateDates(List<TourDate> tourDateS, Tour tour)
        {
            tour.Dates.AddRange(tourDateS.FindAll(x => x.TourId == tour.Id));
        }

        private static void AssociateKeyPoints(List<TourKeyPoint> tourKeyPoints, List<KeyPoint> keyPoints, Tour tour)
        {
            List<TourKeyPoint> pairs = tourKeyPoints.FindAll(x => x.TourId == tour.Id);
            foreach (var pair in pairs)
            {
                KeyPoint matchingKeyPoint = keyPoints.Find(x => x.Id == pair.KeyPointId);
                tour.KeyPoints.Add(matchingKeyPoint);
            }
        }

        public int NextId() { return _tours.Max(x => x.Id) + 1; }
        public List<Tour> GetAll() { return _tours; }

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

        public List<TourDate> FindTodaysDatesById(int TourId)
        {
            Tour? tour = _tours.Find(x => x.Id == TourId);
            List<TourDate> todaysDates = tour.Dates.FindAll(x => x.Date.Date == DateTime.Today.Date);
            return todaysDates;
        }

        public Tour EndTour(int tourId, int dateId)
        {
            Tour toEnd = _tours.Find(x => x.Id == tourId);
            if (toEnd == null) return null;
            _fileHandler.Save(_tours);
            return toEnd;
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

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

    }
}
