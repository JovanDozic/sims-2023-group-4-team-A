using SIMSProject.FileHandler;
using SIMSProject.Observer;
using System.Collections.Generic;

namespace SIMSProject.Model.DAO
{
    public class TourGuestDAO : ISubject
    {
        private List<IObserver> _observers;
        private readonly TourGuestFileHandler _fileHandler;
        private List<TourGuest> _tourGuests;

        public TourGuestDAO()
        {
            _fileHandler = new();
            _tourGuests = _fileHandler.Load();
            _observers = new();

            AssociateTourGuests();

        }

        public List<TourGuest> GetAll() { return _tourGuests; }
        public TourGuest Save(TourGuest tourGuest)
        {
            _tourGuests.Add(tourGuest);
            _fileHandler.Save(_tourGuests);
            NotifyObservers();
            return tourGuest;
        }

        public void SaveAll(List<TourGuest> tourGuests)
        {
            _fileHandler.Save(tourGuests);
            _tourGuests = tourGuests;
            NotifyObservers();
        }


        private void AssociateTourGuests()
        {

            foreach (TourGuest tourGuest in _tourGuests)
            {
                AssociateDate(tourGuest);
                AssociateJoinedKeyPoint(tourGuest);
            }
        }

        private static void AssociateJoinedKeyPoint(TourGuest tourGuest)
        {
            KeyPointFileHandler keyPointFileHandler = new();
            List<KeyPoint> keyPoints = keyPointFileHandler.Load();


            KeyPoint? keyPoint = keyPoints.Find(x => x.Id == tourGuest.JoinedKeyPointId);
            if (keyPoint == null) return;
            tourGuest.JoinedKeyPoint = keyPoint;
        }

        private static void AssociateDate(TourGuest tourGuest)
        {
            TourDateFileHandler dateHandler = new();
            List<TourDate> tourDates = dateHandler.Load();

            TourDate? tourDate = tourDates.Find(x => x.Id == tourGuest.TourDateId);
            if(tourDate == null) return;
            tourGuest.TourDate = tourDate;
        }


        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
