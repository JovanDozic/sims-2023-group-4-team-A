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

            List<Location> locations = _locationFileHandler.Load();
            foreach (var accommodation in _accommodations) accommodation.Location = locations.Find(x => x.Id == accommodation.Location.Id) ?? new Location(accommodation.Location.Id, "<null>", "<null>");
        }

        public int NextId() { return _accommodations.Max(x => x.Id) + 1; }
        public List<Accommodation> GetAll() { return _accommodations; }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            accommodation = LoadImagesToLocalFolder(accommodation);
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

        public Accommodation LoadImagesToLocalFolder(Accommodation accommodation)
        {
            for (int i = 0; i < accommodation.ImageURLs.Count; i++) accommodation.ImageURLs[i] = _fileHandler.SaveImage(accommodation.ImageURLs[i], accommodation.Id);
            return accommodation;
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
