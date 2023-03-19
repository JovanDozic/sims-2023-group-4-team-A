using SIMSProject.Observer;
using SIMSProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model.DAO
{
    public class AccommodationDAO : ISubject
    {
        private List<IObserver> _observers;
        private AccommodationFileHandler _fileHandler;
        private LocationFileHandler _locationFileHandler;
        private List<Accommodation> _accommodations;

        public AccommodationDAO()
        {
            _fileHandler = new();
            _locationFileHandler = new();
            _accommodations = _fileHandler.Load();
            _observers = new();

            var locations = _locationFileHandler.Load();
            var reservations = new AccommodationReservationDAO().GetAll();
            foreach (var accommodation in _accommodations)
            {
                accommodation.Location = locations.Find(x => x.Id == accommodation.Location.Id)
                    ?? new Location(accommodation.Location.Id, "<null>", "<null>");
                accommodation.Reservations = reservations.FindAll(x => x.Accommodation.Id == accommodation.Id);
            }
        }

        public int NextId() { return _accommodations.Max(x => x.Id) + 1; }
        public List<Accommodation> GetAll() { return _accommodations; }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations.Add(accommodation);
            _fileHandler.Save(_accommodations);
            NotifyObservers();
            return accommodation;
        }

        public void SaveAll(List<Accommodation> accommodation)
        {
            _fileHandler.Save(accommodation);
            _accommodations = accommodation;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
