using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Model.DAO;
using SIMSProject.Observer;
using SIMSProject.Repository;

namespace SIMSProject.Model.DAO
{
    public class LocationDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourLocationRepository _repository;
        private List<Location> _tourLocations;

        public LocationDAO()
        {
            _repository = new();
            _tourLocations = _repository.Load();
            _observers = new();
        }

        public List<Location> GetAll() { return _tourLocations; }

        public Location Save(Location tourLocation)
        {
            _tourLocations.Add(tourLocation);
            _repository.Save(_tourLocations);
            NotifyObservers();
            return tourLocation;
        }

        public void SaveAll(List<Location> tourLocations)
        {
            _repository.Save(tourLocations);
            _tourLocations = tourLocations;
            NotifyObservers();
        }

        public Location Get(int id)
        {
            return _tourLocations.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
