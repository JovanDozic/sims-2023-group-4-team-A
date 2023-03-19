using System.Collections.Generic;
using System.Linq;
using SIMSProject.Observer;
using SIMSProject.FileHandler;
using SIMSProject.Controller;
using System.Windows.Navigation;
using System;

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
        public List<TourDate> GetAllByTourId(int tourId)
        {
            List<TourDate> dates = new();
            foreach(TourDate date in GetAll())
            {
                if(date.TourId == tourId)
                {
                    dates.Add(date);
                }
            }
            return dates;
        }

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
        public void UpdateAvailableSpots(TourDate tourDate)
        {
            TourDate? oldTourDate = _tourDates.Find(x => x.Id == tourDate.Id);
            if (oldTourDate == null) return;
            oldTourDate.AvailableSpots = tourDate.AvailableSpots;
            _fileHandler.Save(_tourDates);
            NotifyObservers();
        }

        public void UpdateTourDate(TourDate tourDate)
        {
            TourDate? oldTourDate = _tourDates.Find(x => x.Id == tourDate.Id);
            if (oldTourDate == null) return;
            oldTourDate.TourStatus = tourDate.TourStatus;
            oldTourDate.CurrentKeyPoint = tourDate.CurrentKeyPoint;
            oldTourDate.CurrentKeyPointId = tourDate.CurrentKeyPointId;
            _fileHandler.Save(_tourDates);
        }

        public void EndTourDate(int tourDateId)
        {
            TourDate? tourDate = _tourDates.Find(x => x.Id == tourDateId);
            if (tourDate == null) return;
            tourDate.TourStatus = "Završena";
            _fileHandler.Save(_tourDates);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }

        
    }
}
