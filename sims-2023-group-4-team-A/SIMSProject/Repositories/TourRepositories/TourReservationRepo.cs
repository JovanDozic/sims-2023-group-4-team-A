using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourReservationRepo : ITourReservationRepo
    {
        private readonly TourReservationFileHandler _fileHandler;
        private readonly ITourAppointmentRepo _appointmentRepo;
        private List<TourReservation> _tourReservations;
        public TourReservationRepo(ITourAppointmentRepo appointmentRepo)
        {
            _fileHandler = new TourReservationFileHandler();
            _tourReservations = _fileHandler.Load();
            _appointmentRepo = appointmentRepo;
            MapAppointments();
        }
        public List<TourReservation> GetAll()
        {
            return _tourReservations;
        }

        public List<TourReservation> GetAllByGuestId(int guestId)
        {
            return _tourReservations.FindAll(x => x.GuestId == guestId);
        }

        public TourReservation GetById(int reservationId)
        {
            return _tourReservations.Find(x => x.Id == reservationId);
        }

        public int NextId()
        {
            return _tourReservations.Count > 0 ? _tourReservations.Max(x => x.Id) + 1 : 1;
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservations.Add(tourReservation);
            _fileHandler.Save(_tourReservations);
            return tourReservation;
        }

        public void SaveAll(List<TourReservation> tourReservations)
        {
            _fileHandler.Save(tourReservations);
            _tourReservations = tourReservations;
        }

        public void Update(TourReservation tourReservation)
        {
            TourReservation tourReservationToUpdate = GetById(tourReservation.Id) ?? throw new Exception ("Updating tour reservation failed!");
            int index = _tourReservations.IndexOf(tourReservationToUpdate);
            _tourReservations[index] = tourReservation;
            _fileHandler.Save(_tourReservations);
        }

        private void MapAppointments()
        {
            foreach (var tourReservation in _tourReservations)
            {
                tourReservation.TourAppointment = _appointmentRepo.GetById(tourReservation.TourAppointment.Id);
            }
        }
    }
}
