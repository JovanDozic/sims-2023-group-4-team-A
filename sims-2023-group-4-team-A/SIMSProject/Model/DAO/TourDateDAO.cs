using System.Collections.Generic;
using System.Linq;
using SIMSProject.Model;
using SIMSProject.Observer;
using SIMSProject.Repository;

namespace SIMSProject.Model.DAO
{
    public class TourDateDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourDateRepository _repository;
        private List<TourDate> _tourDates;

        public TourDateDAO()
        {
            _repository = new();
            _tourDates = _repository.Load();
            _observers = new();
        }

        public int NextId() { return _tourDates.Max(x => x.Id) + 1; }
        public List<TourDate> GetAll() { return _tourDates; }

        public TourDate Save(TourDate tourDate)
        {
            tourDate.Id = NextId();
            _tourDates.Add(tourDate);
            _repository.Save(_tourDates);
            NotifyObservers();
            return tourDate;
        }

        public void SaveAll(List<TourDate> tourDates)
        {
            _repository.Save(tourDates);
            _tourDates = tourDates;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
