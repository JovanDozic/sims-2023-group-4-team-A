using SIMSProject.Model;
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
            TourLocationRepository tourLocationRepository = new();
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
