using System.Collections.Generic;
using System.Linq;
using SIMSProject.Model.DAO;
using SIMSProject.Observer;
using SIMSProject.FileHandler;

namespace SIMSProject.Model.DAO
{
    public class TourGuestDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourGuestFileHandler _fileHandler;
        private List<TourGuest> _tourGuests;

        public TourGuestDAO()
        {
            _fileHandler = new();
            _tourGuests = _fileHandler.Load();
            _observers = new();
        }
        public List<TourGuest> GetAll() { return _tourGuests; }

        public TourGuest Save(TourGuest tourGuest)
        {
            _tourGuests.Add(tourGuest);
            _fileHandler.Save(_tourGuests);
            NotifyObservers();
            return tourGuest;
        }

        public void SaveAll(List<TourGuest> tourGuests)
        {
            _fileHandler.Save(tourGuests);
            _tourGuests = tourGuests;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
