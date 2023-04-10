using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.FileHandler;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class AccommodationReservationRepo : IAccommodationReservationRepo
    {
        private AccommodationReservationFileHandler _fileHandler;
        private List<AccommodationReservation> _reservations;

        public AccommodationReservationRepo()
        {
            _fileHandler = new();
            _reservations = _fileHandler.Load();

            MapAccommodations();
            // TODO: call mapping funkcions
        }

        public List<AccommodationReservation> GetAll()
        {
            return _reservations;
        }

        public List<AccommodationReservation> GetAllByAccommodationId(int accommodationId)
        {
            return _reservations.FindAll(x => x.Accommodation.Id == accommodationId);
        }

        public AccommodationReservation GetById(int reservationId)
        {
            return _reservations.Find(x => x.Id == reservationId);
        }

        public int NextId()
        {
            try
            {
                return _reservations.Max(x => x.Id) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public AccommodationReservation Save(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _reservations.Add(reservation);
            _fileHandler.Save(_reservations);
            return reservation;
        }

        public void SaveAll(List<AccommodationReservation> reservations)
        {
            _fileHandler.Save(reservations);
            _reservations = reservations;
        }

        public void MapAccommodations()
        {
            // TODO: Proveri da li je u redu ovako:
            AccommodationRepo repo = new AccommodationRepo();
            foreach (var reservation in _reservations)
            {
                reservation.Accommodation = repo.GetById(reservation.Accommodation.Id);
            }

            // Ili da se koristi FileHandler:
            //var accommodations = new AccommodationFileHandler().Load();
            //foreach (var reservation in _reservations)
            //{
            //    reservation.Accommodation = accommodations.Find(x => x.Id == reservation.Accommodation.Id) ?? new();
            //}
        }

        public void MapGuests()
        {
            // TODO: implement
        }
    }
}
