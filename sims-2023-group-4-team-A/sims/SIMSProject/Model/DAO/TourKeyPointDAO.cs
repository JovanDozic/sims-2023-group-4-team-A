using System.Collections.Generic;
using System.Linq;
using SIMSProject.Model.DAO;
using SIMSProject.Observer;
using SIMSProject.FileHandler;

namespace SIMSProject.Model.DAO
{
    public class TourKeyPointDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourKeyPointFileHandler _fileHandler;
        private List<TourKeyPoint> _tourKeyPoints;

        public TourKeyPointDAO()
        {
            _fileHandler = new();
            _tourKeyPoints = _fileHandler.Load();
            _observers = new();
        }

        
        public List<TourKeyPoint> GetAll() { return _tourKeyPoints; }

        public TourKeyPoint Save(TourKeyPoint tourKeyPoint)
        {
           
            _tourKeyPoints.Add(tourKeyPoint);
            _fileHandler.Save(_tourKeyPoints);
            NotifyObservers();
            return tourKeyPoint;
        }

        public void SaveAll(List<TourKeyPoint> tourKeyPoints)
        {
            _fileHandler.Save(tourKeyPoints);
            _tourKeyPoints = tourKeyPoints;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
