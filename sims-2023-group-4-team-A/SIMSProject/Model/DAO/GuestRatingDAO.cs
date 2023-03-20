using System.Collections.Generic;
using System.Linq;
using SIMSProject.FileHandler;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO
{
    public class GuestRatingDAO
    {
        private readonly List<IObserver> _observers;
        private readonly GuestRatingFileHandler _fileHandler;
        private List<GuestRating> _guestRatings;

        public GuestRatingDAO()
        {
            _fileHandler = new GuestRatingFileHandler();
            _guestRatings = _fileHandler.Load();
            _observers = new List<IObserver>();

            var reservations = new AccommodationReservationFileHandler().Load();
            foreach (var rating in _guestRatings)
            {
                rating.Reservation = reservations.Find(x => x.Id == rating.Reservation.Id) ??
                                     new AccommodationReservation();
            }
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatings;
        }

        public int NextId()
        {
            try
            {
                return _guestRatings.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public GuestRating Save(GuestRating guestRating)
        {
            guestRating.Id = NextId();
            _guestRatings.Add(guestRating);
            _fileHandler.Save(_guestRatings);
            NotifyObservers();
            return guestRating;
        }

        public void SaveAll(List<GuestRating> guestRatings)
        {
            _fileHandler.Save(guestRatings);
            _guestRatings = guestRatings;
            NotifyObservers();
        }

        public GuestRating Get(int id)
        {
            return _guestRatings.Find(x => x.Id == id);
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