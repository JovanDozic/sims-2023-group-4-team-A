using SIMSProject.Observer;
using SIMSProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SIMSProject.Model.DAO
{
    public class KeyPointDAO : ISubject
    {
        private List<IObserver> _observers;
        private KeyPointFileHandler _fileHandler;
        private List<KeyPoint> _keyPoints;

        public KeyPointDAO()
        {
            _fileHandler = new();
            _keyPoints = _fileHandler.Load();
            _observers = new();

            AssociatePoints();
        }

        public int NextId() { return _keyPoints.Max(x => x.Id) + 1; }
        public List<KeyPoint> GetAll() { return _keyPoints; }

        public KeyPoint Save(KeyPoint keyPoint)
        {
            keyPoint.Id = NextId();
            _keyPoints.Add(keyPoint);
            _fileHandler.Save(_keyPoints);
            NotifyObservers();
            return keyPoint;
        }

        public void SaveAll(List<KeyPoint> keyPoints)
        {
            _fileHandler.Save(keyPoints);
            _keyPoints = keyPoints;
            NotifyObservers();
        }

        private void AssociatePoints()
        {

            foreach (var keyPoint in _keyPoints)
            {
                AssociateLocation(keyPoint);
                AssociateTours(keyPoint);
            }
        }

        private static void AssociateLocation(KeyPoint keyPoint)
        {
            LocationFileHandler tourLocationFileHandler = new();
            List<Location> toursLocations = tourLocationFileHandler.Load();

            Location? matchingLocation = toursLocations.Find(x => x.Id == keyPoint.LocationId);
            if (matchingLocation == null) return;
            keyPoint.LocationId = matchingLocation.Id;
            keyPoint.Location = matchingLocation;
        }

        private static void AssociateTours(KeyPoint keyPoint)
        {
            TourFileHandler tourFileHandler = new();
            List<Tour> tours = tourFileHandler.Load();
            TourKeyPointFileHandler tourKeyPointFileHandler = new();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointFileHandler.Load();

            List<TourKeyPoint> pairs = tourKeyPoints.FindAll(x => x.KeyPointId == keyPoint.Id);
            foreach (var pair in pairs)
            {
                Tour? matchingTour = tours.Find(x => x.Id == pair.TourId);
                if(matchingTour == null) continue;
                keyPoint.Tours.Add(matchingTour);
            }
        }

        

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }

}
