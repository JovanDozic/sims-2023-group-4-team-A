using SIMSProject.Observer;
using SIMSProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model.DAO
{
    public class AccommodationDAO : ISubject
    {
        private List<IObserver> _observers;
        private AccommodationRepository _repository;
        private List<Accommodation> _accommodations;

        public AccommodationDAO()
        {
            _repository = new();
            _accommodations = _repository.Load();
            _observers = new();
        }

        public int NextId() { return _accommodations.Max(x => x.Id) + 1; }
        public List<Accommodation> GetAll() { return _accommodations; }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _repository.Save(_accommodations);
            NotifyObservers();
            return accommodation;
        }

        public void SaveAll(List<Accommodation> accommodation)
        {
            _repository.Save(accommodation);
            _accommodations = accommodation;
            NotifyObservers();
        }



        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }


    }
}
