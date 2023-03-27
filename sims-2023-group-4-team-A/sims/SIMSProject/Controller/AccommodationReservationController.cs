using System.Collections.Generic;
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
    }
}