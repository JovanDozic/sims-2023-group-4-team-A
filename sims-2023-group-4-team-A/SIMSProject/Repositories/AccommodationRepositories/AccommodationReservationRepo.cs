using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.AccommodationFileHandlers;
using SIMSProject.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.AccommodationRepositories
{
    public class AccommodationReservationRepo : IAccommodationReservationRepo
    {
        private readonly AccommodationReservationFileHandler _fileHandler;
        private readonly IAccommodationRepo _accommodationRepo;
        private readonly IGuest1Repo _guestRepo;
        private List<AccommodationReservation> _reservations;

        public AccommodationReservationRepo(IAccommodationRepo accommodationRepo, IGuest1Repo guestRepo)
        {
            _fileHandler = new();
            _reservations = new();
            _accommodationRepo = accommodationRepo;
            _guestRepo = guestRepo;

            Load();
        }

        public void Load()
        {
            _reservations = _fileHandler.Load();

            MapAccommodations();
            MapGuests();
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
            return _reservations.Count > 0 ? _reservations.Max(x => x.Id) + 1 : 1;
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

        public void Update(AccommodationReservation reservation)
        {
            AccommodationReservation reservationToUpdate = GetById(reservation.Id) ?? throw new Exception("Updating accommodation reservation failed!");
            int index = _reservations.IndexOf(reservationToUpdate);
            _reservations[index] = reservation;
            _fileHandler.Save(_reservations);
        }

        private void MapAccommodations()
        {
            foreach (var reservation in _reservations)
            {
                reservation.Accommodation = _accommodationRepo.GetById(reservation.Accommodation.Id);
            }
        }

        private void MapGuests()
        {
            foreach (var reservation in _reservations)
            {
                reservation.Guest = _guestRepo.GetById(reservation.Guest.Id);
            }
        }
    }
}
