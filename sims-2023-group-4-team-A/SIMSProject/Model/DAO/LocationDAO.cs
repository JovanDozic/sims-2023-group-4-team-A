using System.Collections.Generic;
using System.Linq;
using SIMSProject.Model;
using SIMSProject.Model.DAO;
using SIMSProject.Observer;
using SIMSProject.FileHandler;

namespace SIMSProject.Model.DAO
{
    public class LocationDAO : ISubject
    {
        private List<IObserver> _observers;
        private LocationFileHandler _fileHandler;
        private List<Location> _locations;

        public LocationDAO()
        {
            _fileHandler = new();
            _locations = _fileHandler.Load();
            _observers = new();
        }

        public List<Location> GetAll() { return _locations; }
        public int NextId() { return _locations.Max(x => x.Id) + 1; }
        
        public Location Save(Location location)
        {
            location.Id = NextId();
            _locations.Add(location);
            _fileHandler.Save(_locations);
            NotifyObservers();
            return location;
        }

        public void SaveAll(List<Location> locations)
        {
            _fileHandler.Save(locations);
            _locations = locations;
            NotifyObservers();
        }

        public Location Get(int id)
        {
            return _locations.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
