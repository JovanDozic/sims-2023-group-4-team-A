using System.Collections.Generic;
using System.Linq;
using SIMSProject.Observer;
using SIMSProject.FileHandler;
using SIMSProject.Controller;

namespace SIMSProject.Model.DAO
{
    public class TourDateDAO : ISubject
    {
        private List<IObserver> _observers;
        private readonly TourDateFileHandler _fileHandler;
        private List<TourDate> _tourDates;

        public TourDateDAO()
        {
            _fileHandler = new();
            _tourDates = _fileHandler.Load();
            _observers = new();

            AssociateTourDates();

        }

        private void AssociateTourDates()
        {
            TourFileHandler tourFileHandler = new();
            KeyPointFileHandler keyPointFileHandler = new();

            List<Tour> tours = tourFileHandler.Load();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();

            foreach (TourDate date in _tourDates)
            {
                AssociateTour(tours, date);
                AssociateCurrenKeyPoint(keyPoints, date);
            }
        }

        private static void AssociateCurrenKeyPoint(List<KeyPoint> keyPoints, TourDate date)
        {
            KeyPoint current = keyPoints.Find(x => x.Id == date.CurrentKeyPointId);
            date.CurrentKeyPoint = current;
        }

        private static void AssociateTour(List<Tour> tours, TourDate date)
        {
            Tour tour = tours.Find(x => x.Id == date.TourId);
            date.Tour = tour;
        }

        public int NextId() { return _tourDates.Max(x => x.Id) + 1; }
        public List<TourDate> GetAll() { return _tourDates; }

        public TourDate Save(TourDate tourDate)
        {
            tourDate.Id = NextId();
            _tourDates.Add(tourDate);
            _fileHandler.Save(_tourDates);
            NotifyObservers();
            return tourDate;
        }

        public void SaveAll(List<TourDate> tourDates)
        {
            _fileHandler.Save(tourDates);
            _tourDates = tourDates;
            NotifyObservers();
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
