using SIMSProject.Observer;
using SIMSProject.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Model.DAO
{
    public class AddressDAO : ISubject
    {
        private List<IObserver> _observers;
        private AddressRepository _repository;
        private List<Location> _addresses;

        public AddressDAO()
        {
            _repository = new();
            _addresses = _repository.Load();
            _observers = new();
        }

        public int NextId() { return _addresses.Max(x => x.Id) + 1; }
        public List<Location> GetAll() { return _addresses; }

        public Location Save(Location address)
        {
            address.Id = NextId();
            _addresses.Add(address);
            _repository.Save(_addresses);
            NotifyObservers();
            return address;
        }

        public void SaveAll(List<Location> addresses)
        {
            _repository.Save(addresses);
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
