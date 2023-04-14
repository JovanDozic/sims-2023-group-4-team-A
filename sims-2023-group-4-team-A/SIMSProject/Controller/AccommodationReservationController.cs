using System;
using System.Collections.Generic;
using System.Windows;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Model;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class AccommodationReservationController
    {
        private readonly AccommodationReservationDAO _accommodationReservationDAO;
        public AccommodationReservation AccommodationReservation;

        public AccommodationReservationController()
        {
            _accommodationReservationDAO = new AccommodationReservationDAO();
            AccommodationReservation = new AccommodationReservation();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationDAO.GetAll();
        }

        public List<AccommodationReservation> GetAllByAccommodation(Accommodation accommodation)
        {
            return _accommodationReservationDAO.GetAll().FindAll(x => x.Accommodation.Id == accommodation.Id);
        }

        public void SaveAll(List<AccommodationReservation> reservation)
        {
            _accommodationReservationDAO.SaveAll(reservation);
        }

        public AccommodationReservation Create(AccommodationReservation reservation)
        {
            return _accommodationReservationDAO.Save(reservation);
        }

        public void UpdateExisting(AccommodationReservation updatedReservation)
        {
            var reservations = _accommodationReservationDAO.GetAll();
            var reservation = reservations.Find(x => x.Id == updatedReservation.Id);
            if (reservation != null)
            {
                var index = reservations.IndexOf(reservation);
                reservations.Remove(reservation);
                reservations.Insert(index, updatedReservation);
            }
            else
            {
                Create(updatedReservation);
            }

            SaveAll(reservations);
        }

        public void Update(AccommodationReservation updatedReservation)
        {
            var reservations = _accommodationReservationDAO.GetAll();
            int index = reservations.FindIndex(s => s.Id == updatedReservation.Id);
            if (index != -1)
            {
                reservations[index] = updatedReservation;
            }
            SaveAll(reservations);
        }

        public AccommodationReservation FindAndCancel(AccommodationReservation selectedReservation)
        {
            selectedReservation.Canceled = true;

            return selectedReservation;
        }

        public bool IsAvailable(AccommodationReservation reservationToBeMoved, DateRange newDates)
        {
            foreach (var reservation in GetAllByAccommodation(reservationToBeMoved.Accommodation))
            {
                if (reservation.Id == reservationToBeMoved.Id) continue;
                if (newDates.StartDate < reservation.EndDate && newDates.EndDate > reservation.StartDate)
                    return false;
            }
            return true;
        }

    }
}