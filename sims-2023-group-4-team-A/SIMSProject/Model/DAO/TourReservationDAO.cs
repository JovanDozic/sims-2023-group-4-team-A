using SIMSProject.FileHandler;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model.DAO
{
    public class TourReservationDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourReservationFileHandler _fileHandler;
        private List<TourReservation> _tourReservation;

        public TourReservationDAO()
        {
            _fileHandler = new();
            _tourReservation = _fileHandler.Load();
            _observers = new();
        }
        public List<TourReservation> GetAll() { return _tourReservation; }

        public TourReservation Save(TourReservation tourReservation)
        {
            _tourReservation.Add(tourReservation);
            _fileHandler.Save(_tourReservation);
            NotifyObservers();
            return tourReservation;
        }

        public void SaveAll(List<TourReservation> tourReservation)
        {
            _fileHandler.Save(tourReservation);
            _tourReservation = tourReservation;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

    }
}
