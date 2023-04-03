using SIMSProject.FileHandler;
using SIMSProject.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Model.DAO
{
    public class ReschedulingRequestDAO
    {
        private readonly List<IObserver> _observers;
        private readonly ReschedulingRequestFileHandler _fileHandler;
        private List<ReschedulingRequest> _reschedulingRequests;
        private AccommodationReservationDAO _reservationDAO;

        public ReschedulingRequestDAO()
        {
            _observers = new();
            _fileHandler = new();
            _reschedulingRequests = _fileHandler.Load();
            _reservationDAO = new();

            MapReschedulingRequests();
        }

        public void MapReschedulingRequests()
        {
            _reschedulingRequests.ForEach(x => x.AccommodationReservation = _reservationDAO.GetById(x.AccommodationReservation.Id));
        }

        public int NextId()
        {
            try
            {
                return _reschedulingRequests.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<ReschedulingRequest> GetAll()
        {
            return _reschedulingRequests;
        }

        public ReschedulingRequest GetById(int requestId)
        {
            return _reschedulingRequests.Find(x => x.Id == requestId) ?? new ReschedulingRequest();
        }

        public ReschedulingRequest Save(ReschedulingRequest ReschedulingRequest)
        {
            ReschedulingRequest.Id = NextId();
            _reschedulingRequests.Add(ReschedulingRequest);
            _fileHandler.Save(_reschedulingRequests);
            NotifyObservers();
            return ReschedulingRequest;
        }

        public void SaveAll(List<ReschedulingRequest> requests)
        {
            _fileHandler.Save(_reschedulingRequests);
            _reschedulingRequests = requests;
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
