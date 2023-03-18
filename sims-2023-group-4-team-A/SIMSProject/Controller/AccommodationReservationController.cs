using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model.DAO;
using SIMSProject.Model;

namespace SIMSProject.Controller
{
    public class AccommodationReservationController
    {
        private AccommodationReservationDAO _accommodationReservationDAO;
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

        public void SaveAll(List<AccommodationReservation> accommodationReservation)
        {
            _accommodationReservationDAO.SaveAll(accommodationReservation);
        }
        public AccommodationReservation Create(AccommodationReservation accommodationReservation)
        {
            return _accommodationReservationDAO.Save(accommodationReservation);
        }
    }
}
