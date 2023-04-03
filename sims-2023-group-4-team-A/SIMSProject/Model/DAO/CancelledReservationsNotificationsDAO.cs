using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model;
using SIMSProject.Serializer;
using SIMSProject.FileHandler;
using SIMSProject.Observer;

namespace SIMSProject.Model.DAO
{
    public class CancelledReservationsNotificationsDAO
    {
        private readonly List<IObserver> _observers;
        private readonly CancelledReservationsNotificationsFileHandler _fileHandler;
        private List<CancelledReservationsNotifications> _cancelledReservationsNotifications;

        public CancelledReservationsNotificationsDAO()
        {
            _observers = new List<IObserver>();
            _fileHandler = new CancelledReservationsNotificationsFileHandler();
            _cancelledReservationsNotifications = _fileHandler.Load();
        }

        public int NextId()
        {
            try
            {
                return _cancelledReservationsNotifications.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<CancelledReservationsNotifications> GetAll()
        {
            return _cancelledReservationsNotifications;
        }

        public CancelledReservationsNotifications Save(CancelledReservationsNotifications CancelledReservationsNotifications)
        {
            CancelledReservationsNotifications.Id = NextId();
            _cancelledReservationsNotifications.Add(CancelledReservationsNotifications);
            _fileHandler.Save(_cancelledReservationsNotifications);
            NotifyObservers();
            return CancelledReservationsNotifications;
        }

        public void SaveAll(List<CancelledReservationsNotifications> CancelledReservationsNotifications)
        {
            _fileHandler.Save(CancelledReservationsNotifications);
            _cancelledReservationsNotifications = CancelledReservationsNotifications;
            NotifyObservers();
        }

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
