using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Observer;
using SIMSProject.FileHandler;

namespace SIMSProject.Model.DAO
{
    
    public class AccommodationReservationDAO: ISubject
    {
        private List<IObserver> _observers;
        private AccommodationReservationFileHandler _fileHandler;
        private LocationFileHandler _locationFileHandler;
        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationDAO()
        {
            _observers = new List<IObserver>();
            _fileHandler = new AccommodationReservationFileHandler();
            _locationFileHandler = new LocationFileHandler();
            _accommodationReservations = _fileHandler.Load();

            
        }

        public int NextId()
        {
            return _accommodationReservations.Max(x => x.Id) + 1;
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations;
        }

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
            accommodationReservation.Id = NextId();
            _accommodationReservations.Add(accommodationReservation);
            _fileHandler.Save(_accommodationReservations);
            NotifyObservers();
            return accommodationReservation;
        }

        public void SaveAll(List<AccommodationReservation> accommodationReservation)
        {
            _fileHandler.Save(accommodationReservation);
            _accommodationReservations = accommodationReservation;
            NotifyObservers();
        }

        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
