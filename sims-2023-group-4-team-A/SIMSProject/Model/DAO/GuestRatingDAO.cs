using SIMSProject.FileHandler;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model.DAO
{
    public class GuestRatingDAO
    {
        private List<IObserver> _observers;
        private GuestRatingFileHandler _fileHandler;
        private List<GuestRating> _guestRatings;

        public GuestRatingDAO()
        {
            _fileHandler = new();
            _guestRatings = _fileHandler.Load();
            _observers = new();
        }

        public List<GuestRating> GetAll() { return _guestRatings; }
        public int NextId() { return _guestRatings.Max(x => x.Id) + 1; }

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
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
