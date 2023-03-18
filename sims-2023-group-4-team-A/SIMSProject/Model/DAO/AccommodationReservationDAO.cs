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
        private List<AccommodationReservation> _AccommodationReservations;

        public AccommodationReservationDAO()
        {
            _observers = new List<IObserver>();
            _fileHandler = new AccommodationReservationFileHandler();
            _locationFileHandler = new LocationFileHandler();
            _AccommodationReservations = _fileHandler.Load();

            
        }

        public int NextId()
        {
            return _AccommodationReservations.Max(x => x.Id) + 1;
        }

        public List<AccommodationReservation> GetAll()
        {
            return _AccommodationReservations;
        }

        public AccommodationReservation Save(AccommodationReservation AccommodationReservation)
        {
            AccommodationReservation.Id = NextId();
            _AccommodationReservations.Add(AccommodationReservation);
            _fileHandler.Save(_AccommodationReservations);
            NotifyObservers();
            return AccommodationReservation;
        }

        public void SaveAll(List<AccommodationReservation> AccommodationReservation)
        {
            _fileHandler.Save(AccommodationReservation);
            _AccommodationReservations = AccommodationReservation;
            NotifyObservers();
        }

        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
