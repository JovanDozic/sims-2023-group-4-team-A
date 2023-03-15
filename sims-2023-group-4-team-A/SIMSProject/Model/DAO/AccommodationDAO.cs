using SIMSProject.Observer;
using SIMSProject.Repository;
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
        private AccommodationRepository _repository;
        private LocationRepository _locationRepository;
        private List<Accommodation> _accommodations;

        public AccommodationDAO()
        {
            _repository = new();
            _locationRepository = new();
            _accommodations = _repository.Load();
            _observers = new();

            List<Location> locations = _locationRepository.Load();
            foreach (var accommodation in _accommodations) accommodation.Location = locations.Find(x => x.Id == accommodation.Location.Id) ?? new Location(accommodation.Location.Id, "<null>", "<null>");
        }

        public int NextId() { return _accommodations.Max(x => x.Id) + 1; }
        public List<Accommodation> GetAll() { return _accommodations; }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            accommodation = LoadImagesToLocalFolder(accommodation);
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

        public Accommodation LoadImagesToLocalFolder(Accommodation accommodation)
        {
            for (int i = 0; i < accommodation.ImageURLs.Count; i++) accommodation.ImageURLs[i] = _repository.SaveImage(accommodation.ImageURLs[i], accommodation.Id);
            return accommodation;
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
