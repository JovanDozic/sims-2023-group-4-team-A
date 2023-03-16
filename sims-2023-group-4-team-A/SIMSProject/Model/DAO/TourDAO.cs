using SIMSProject.Observer;
using SIMSProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace SIMSProject.Model.DAO
{
    public class TourDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourRepository _repository;
        private List<Tour> _tours;

        public TourDAO()
        {
            _repository = new();
            _tours = _repository.Load();
            _observers = new();

            AssociateTours();            
        }

        private void AssociateTours()
        {
            LocationRepository tourLocationRepository = new();
            TourKeyPointRepository tourKeyPointRepository = new();
            KeyPointRepository keyPointRepository = new();
            TourDateRepository tourDateRepository = new();

            List<Location> tourLocations = tourLocationRepository.Load();
            List<TourDate> tourDateS = tourDateRepository.Load();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointRepository.Load();
            List<KeyPoint> keyPoints = keyPointRepository.Load();


            foreach (var tour in _tours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
                tour.Dates.AddRange(tourDateS.FindAll(x => x.TourId == tour.Id));


                List<TourKeyPoint> pairs = tourKeyPoints.FindAll(x => x.TourId == tour.Id);
                foreach (var pair in pairs)
                {
                    KeyPoint matchingKeyPoint = keyPoints.Find(x => x.Id == pair.KeyPointId);
                    tour.KeyPoints.Add(matchingKeyPoint);
                }

            }
        }

        public int NextId() { return _tours.Max(x => x.Id) + 1; }
        public List<Tour> GetAll() { return _tours; }

        /*public static List<Tour> Search(string searchText)
        {
            // učitaj podatke iz izvora podataka
            List<Tour> allTours = GetAll();
            // podeli pretražni tekst na dva dela, ime i prezime
            var searchTerms = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = searchTerms.Length > 0 ? searchTerms[0] : string.Empty;
            var lastName = searchTerms.Length > 1 ? searchTerms[1] : string.Empty;
            // filtriraj podatke na osnovu pretražnih pojmova
            var filteredData = allData.Where(d => d.FirstName.Contains(firstName) && d.LastName.Contains(lastName));
            // mapiraj filtrirane podatke u rezultate
            var results = filteredData.Select(d => new Result(d));
            return results.ToList();
        }*/

        public List<Tour> SearchLocations(string locationId)
        {
            return _tours.Where(tour => tour.LocationId.Equals(locationId)).ToList();
        }

        public List<Tour> SearchDurations(string duration)
        {
            return _tours.Where(tour => tour.Duration.Equals(duration)).ToList();
        }

        public List<Tour> SearchLanguages(string language)
        {
            return _tours.Where(tour => tour.TourLanguage.Equals(language)).ToList();
        }

        public List<Tour> SearchMaxGuests(string maxGuests)
        {
            return _tours.Where(tour => tour.MaxGuestNumber.Equals(maxGuests)).ToList();
        }

        public List<Tour> FindTodaysTours()
        {
            return _tours.FindAll(x => x.Dates.Any(x => x.Date.Date == DateTime.Today.Date));
        }

        public Tour EndTour(int tourId, int dateId)
        {
            Tour toEnd = _tours.Find(x => x.Id == tourId);
            if (toEnd == null) return null;
            toEnd.TourState = "Završena";
            _repository.Save(_tours);
            return toEnd;
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours.Add(tour);
            _repository.Save(_tours);
            NotifyObservers();
            return tour;
        }

        public void SaveAll(List<Tour> tours)
        {
            _repository.Save(tours);
            _tours = tours;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

    }
}
