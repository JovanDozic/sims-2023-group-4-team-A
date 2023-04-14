using System.Collections.Generic;
using System.Linq;
using SIMSProject.FileHandler;
using SIMSProject.Observer;
using SIMSProject.Domain.Models.TourModels;


namespace SIMSProject.Model.DAO
{
    public class TourReservationDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly TourReservationFileHandler _fileHandler;
        private List<TourReservation> _tourReservations;

        public TourReservationDAO()
        {
            _fileHandler = new TourReservationFileHandler();
            _tourReservations = _fileHandler.Load();
            _observers = new List<IObserver>();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservations;
        }

        public int NextId()
        {
            try
            {
                return _tourReservations.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservations.Add(tourReservation);
            _fileHandler.Save(_tourReservations);
            NotifyObservers();
            return tourReservation;
        }

        public void SaveAll(List<TourReservation> tourReservation)
        {
            _fileHandler.Save(tourReservation);
            _tourReservations = tourReservation;
            NotifyObservers();
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