using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandler;
using SIMSProject.Model;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class ReschedulingRequestRepo : IReschedulingRequestRepo
    {
        private readonly ReschedulingRequestFileHandler _fileHandler;
        private readonly IAccommodationReservationRepo _reservationRepo;
        private List<ReschedulingRequest> _requests;

        public ReschedulingRequestRepo(IAccommodationReservationRepo reservationRepo)
        {
            _fileHandler = new();
            _requests = new();
            _reservationRepo = reservationRepo;

            Load();
        }

        public void Load()
        {
            _requests = _fileHandler.Load();
            MapReservations();
        }

        public List<ReschedulingRequest> GetAll()
        {
            return _requests;
        }

        public ReschedulingRequest GetById(int requestId)
        {
            return _requests.Find(x => x.Id == requestId);
        }

        public List<ReschedulingRequest> GetAllByOwnerId(int ownerId)
        {
            return _requests.FindAll(x => x.Reservation != null && x.Reservation.Accommodation.Owner.Id == ownerId);
        }

        public int NextId()
        {
            return _requests.Count > 0 ? _requests.Max(x => x.Id) + 1 : 1;
        }

        public ReschedulingRequest Save(ReschedulingRequest request)
        {
            request.Id = NextId();
            _requests.Add(request);
            _fileHandler.Save(_requests);
            return request;
        }

        public void SaveAll(List<ReschedulingRequest> requests)
        {
            _fileHandler.Save(requests);
            _requests = requests;
        }

        public void Update(ReschedulingRequest request)
        {
            ReschedulingRequest requestToUpdate = GetById(request.Id) ?? throw new System.Exception("Updating rescheduling request failed!");
            int index = _requests.IndexOf(requestToUpdate);
            _requests[index] = request;
            _fileHandler.Save(_requests);
        }

        private void MapReservations()
        {
            foreach (var request in _requests)
            {
                request.Reservation = _reservationRepo.GetById(request.Reservation.Id);
            }
        }

        public List<ReschedulingRequest> GetAllByGuestId(int guestId)
        {
            return _requests.FindAll(x => x.Reservation != null && x.Reservation.Guest.Id == guestId);

        }
    }
}
