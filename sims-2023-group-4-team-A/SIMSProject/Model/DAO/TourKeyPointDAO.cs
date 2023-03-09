using System.Collections.Generic;
using System.Linq;
using SIMSProject.Model;
using SIMSProject.Model.DAO;
using SIMSProject.Observer;
using SIMSProject.Repository;

namespace SIMSProject.Model.DAO
{
    public class TourKeyPointDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourKeyPointRepository _repository;
        private List<TourKeyPoint> _tourKeyPoints;

        public TourKeyPointDAO()
        {
            _repository = new();
            _tourKeyPoints = _repository.Load();
            _observers = new();
        }

        
        public List<TourKeyPoint> GetAll() { return _tourKeyPoints; }

        public TourKeyPoint Save(TourKeyPoint tourKeyPoint)
        {
           
            _tourKeyPoints.Add(tourKeyPoint);
            _repository.Save(_tourKeyPoints);
            NotifyObservers();
            return tourKeyPoint;
        }

        public void SaveAll(List<TourKeyPoint> tourKeyPoints)
        {
            _repository.Save(tourKeyPoints);
            _tourKeyPoints = tourKeyPoints;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
