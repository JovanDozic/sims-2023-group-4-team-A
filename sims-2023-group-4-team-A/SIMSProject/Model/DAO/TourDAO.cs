using SIMSProject.Observer;
using SIMSProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using System.Diagnostics;

namespace SIMSProject.Model.DAO
{
    public class TourDAO : ISubject
    {
        private List<IObserver> _observers;
        private TourFileHandler _fileHandler;
        private List<Tour> _tours;

        public TourDAO()
        {
            _fileHandler = new();
            _tours = _fileHandler.Load();
            _observers = new();

            AssociateTours();            
        }

        private void AssociateTours()
        {
            LocationFileHandler tourLocationFileHandler = new();
            TourKeyPointFileHandler tourKeyPointFileHandler = new();
            KeyPointFileHandler keyPointFileHandler = new();
            TourDateFileHandler tourDateFileHandler = new();

            List<Location> tourLocations = tourLocationFileHandler.Load();
            List<TourDate> tourDateS = tourDateFileHandler.Load();
            List<TourKeyPoint> tourKeyPoints = tourKeyPointFileHandler.Load();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();


            foreach (var tour in _tours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
                tour.Dates = tourDateS.Where(x => x.TourId == tour.Id).ToList();


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

        

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours.Add(tour);
            _fileHandler.Save(_tours);
            NotifyObservers();
            return tour;
        }

        public void SaveAll(List<Tour> tours)
        {
            _fileHandler.Save(tours);
            _tours = tours;
            NotifyObservers();
        }

        public List<Tour> GetToursWithSameLocation(Tour selectedTour)
        {
            List<Tour> tours = new();
            foreach (Tour tour in GetAll())
            {
                if (tour.Location.Id == selectedTour.Location.Id && tour.Id!=selectedTour.Id)
                {
                    tours.Add(tour);
                }
            }
            return tours;
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

        
    }
}
