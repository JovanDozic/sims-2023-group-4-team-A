using System.Collections.Generic;
using System.Linq;
using SIMSProject.FileHandler;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO
{
    public class AccommodationReservationDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly AccommodationReservationFileHandler _fileHandler;
        private LocationFileHandler _locationFileHandler;
        private List<AccommodationReservation> _accommodationReservations;

        public AccommodationReservationDAO()
        {
            _observers = new List<IObserver>();
            _fileHandler = new AccommodationReservationFileHandler();
            _locationFileHandler = new LocationFileHandler();
            _accommodationReservations = _fileHandler.Load();

            var _accommodations = new AccommodationFileHandler().Load();
            var _guests = new GuestFileHandler().Load();
            foreach (var reservation in _accommodationReservations)
            {
                reservation.Accommodation = _accommodations.Find(x => x.Id == reservation.Accommodation.Id) ??
                                            new Accommodation();
                reservation.Guest = _guests.Find(x => x.Id == reservation.Guest.Id) ?? new Guest(101, "null", "null");
            }
        }

        public int NextId()
        {
            try
            {
                return _accommodationReservations.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservations;
        }

        public AccommodationReservation Save(AccommodationReservation AccommodationReservation)
        {
            AccommodationReservation.Id = NextId();
            _accommodationReservations.Add(AccommodationReservation);
            _fileHandler.Save(_accommodationReservations);
            NotifyObservers();
            return AccommodationReservation;
        }

        public void SaveAll(List<AccommodationReservation> AccommodationReservations)
        {
            _fileHandler.Save(AccommodationReservations);
            _accommodationReservations = AccommodationReservations;
            NotifyObservers();
        }

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