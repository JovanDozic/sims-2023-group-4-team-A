using SIMSProject.Observer;
using SIMSProject.FileHandler;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Model.DAO
{
    public class AddressDAO : ISubject
    {
        private List<IObserver> _observers;
        private AddressFileHandler _fileHandler;
        private List<Location> _addresses;

        public AddressDAO()
        {
            _fileHandler = new();
            _addresses = _fileHandler.Load();
            _observers = new();
        }

        public int NextId() { return _addresses.Max(x => x.Id) + 1; }
        public List<Location> GetAll() { return _addresses; }

        public Location Save(Location address)
        {
            address.Id = NextId();
            _addresses.Add(address);
            _fileHandler.Save(_addresses);
            NotifyObservers();
            return address;
        }

        public void SaveAll(List<Location> addresses)
        {
            _fileHandler.Save(addresses);
            _addresses = addresses;
            NotifyObservers();
        }

        public Location Get(int id)
        {
            return _addresses.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
