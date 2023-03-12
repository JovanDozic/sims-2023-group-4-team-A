using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Model.DAO;
using SIMSProject.Observer;
using SIMSProject.Repository;

namespace SIMSProject.Model.DAO
{
    public class TourLocationDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourLocationRepository _repository;
        private List<TourLocation> _tourLocations;

        public TourLocationDAO()
        {
            _repository = new();
            _tourLocations = _repository.Load();
            _observers = new();
        }

        public List<TourLocation> GetAll() { return _tourLocations; }

        public TourLocation Save(TourLocation tourLocation)
        {
            _tourLocations.Add(tourLocation);
            _repository.Save(_tourLocations);
            NotifyObservers();
            return tourLocation;
        }

        public void SaveAll(List<TourLocation> tourLocations)
        {
            _repository.Save(tourLocations);
            _tourLocations = tourLocations;
            NotifyObservers();
        }

        public TourLocation Get(int id)
        {
            return _tourLocations.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
