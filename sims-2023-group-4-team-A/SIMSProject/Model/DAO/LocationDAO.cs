using System.Collections.Generic;
using System.Linq;
using SIMSProject.Domain.Models;
using SIMSProject.FileHandlers;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO
{
    public class LocationDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly LocationFileHandler _fileHandler;
        private List<Location> _locations;

        public LocationDAO()
        {
            _fileHandler = new LocationFileHandler();
            _locations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public List<Location> GetAll()
        {
            return _locations;
        }

        public int NextId()
        {
            try
            {
                return _locations.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

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