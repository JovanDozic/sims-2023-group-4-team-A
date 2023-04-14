using SIMSProject.Model;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Controller
{
    public class TourReservationController
    {
        private TourReservationDAO _tourReservation;
        public TourReservation TourGuest;
        public TourReservationController()
        {
            _tourReservation = new();
            TourGuest = new();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservation.GetAll();
        }

        public void SaveAll(List<TourReservation> tourReservations)
        {
            _tourReservation.SaveAll(tourReservations);
        }

        public TourReservation Create(TourReservation tourReservation)
        {
            return _tourReservation.Save(tourReservation);
        }
    }
}
