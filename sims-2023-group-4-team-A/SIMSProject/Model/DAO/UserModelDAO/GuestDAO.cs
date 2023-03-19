using SIMSProject.FileHandler;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Model.UserModel;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.Model.DAO.UserModelDAO
{
    public class GuestDAO : ISubject
    {
        private List<IObserver> _observers;
        private GuestFileHandler _fileHandler;
        private List<Guest> _guests;

        public GuestDAO()
        {
            _fileHandler = new();
            _guests = _fileHandler.Load();
            _observers = new();

            var tourReservations = new TourReservationDAO().GetAll();
            foreach (var guest in _guests) guest.TourReservations = tourReservations.FindAll(x => x.GuestId == guest.Id);

            var reservations = new AccommodationReservationFileHandler().Load();
            foreach (var guest in _guests) guest.AccommodationReservations = reservations.FindAll(x => x.Guest.Id == guest.Id);

            RefreshRatings();
        }

        public int NextId() { return _guests.Max(x => x.Id) + 1; }

        public List<Guest> GetAll() { return _guests; }

        public void RefreshRatings()
        {
            var _ratings = new GuestRatingDAO().GetAll();
            foreach (var guest in _guests)
            {
                try
                {
                    guest.Rating = _ratings.FindAll(x => x.Reservation.Guest.Id == guest.Id).Average(x => x.Overall);
                }
                catch
                {
                    guest.Rating = 15;
                }
            }
            SaveAll(_guests);
        }

        public Guest Save(Guest user)
        {
            user.Id = NextId();
            _guests.Add(user);
            _fileHandler.Save(_guests);
            NotifyObservers();
            return user;
        }

        public void SaveAll(List<Guest> users)
        {
            _fileHandler.Save(users);
            _guests = users;
            NotifyObservers();
        }

        public Guest Get(int id)
        {
            return _guests.Find(x => x.Id == id);
        }

        // [OBSERVERS]
        public void NotifyObservers() { foreach (var observer in _observers) observer.Update(); }
        public void Subscribe(IObserver observer) { _observers.Add(observer); }
        public void Unsubscribe(IObserver observer) { _observers.Remove(observer); }
    }
}
