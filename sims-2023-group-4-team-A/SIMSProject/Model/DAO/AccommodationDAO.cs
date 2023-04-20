using System.Collections.Generic;
using System.Linq;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.FileHandlers;
using SIMSProject.Domain.Models;
using SIMSProject.Observer;
using SIMSProject.FileHandlers.AccommodationFileHandlers;

namespace SIMSProject.Model.DAO
{
    public class AccommodationDAO : ISubject
    {
        private readonly List<IObserver> _observers;
        private readonly AccommodationFileHandler _fileHandler;
        private readonly LocationFileHandler _locationFileHandler;
        private List<Accommodation> _accommodations;

        public AccommodationDAO()
        {
            _fileHandler = new AccommodationFileHandler();
            _locationFileHandler = new LocationFileHandler();
            _accommodations = _fileHandler.Load();
            _observers = new List<IObserver>();

            var locations = _locationFileHandler.Load();
            var reservations = new AccommodationReservationDAO().GetAll();
            foreach (var accommodation in _accommodations)
            {
                accommodation.Location = locations.Find(x => x.Id == accommodation.Location.Id)
                                         ?? new Location();
            }
        }

        public int NextId()
        {
            return _accommodations.Count > 0 ? _accommodations.Max(x => x.Id) + 1 : 1;
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

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