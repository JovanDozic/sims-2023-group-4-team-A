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
        private AccommodationReservationDAO _AccommodationReservationDAO;
        public AccommodationReservation AccommodationReservation;

        public AccommodationReservationController()
        {
            _AccommodationReservationDAO = new AccommodationReservationDAO();
            AccommodationReservation = new AccommodationReservation();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _AccommodationReservationDAO.GetAll();
        }

        public void SaveAll(List<AccommodationReservation> AccommodationReservation)
        {
            _AccommodationReservationDAO.SaveAll(AccommodationReservation);
        }
        public AccommodationReservation Create(AccommodationReservation AccommodationReservation)
        {
            return _AccommodationReservationDAO.Save(AccommodationReservation);
        }
    }
}
