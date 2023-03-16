using System.Collections.Generic;
using SIMSProject.Model.DAO;
using SIMSProject.Model;

namespace SIMSProject.Controller
{
    public class TourGuestController
    {
        private TourGuestDAO _tourGuests;
        public Model.Attendance TourGuest;

        public TourGuestController()
        {
            _tourGuests = new();
            TourGuest = new();
        }

        public List<Model.Attendance> GetAll()
        {
            return _tourGuests.GetAll();
        }

        public void SaveAll(List<Model.Attendance> tourGuests)
        {
            _tourGuests.SaveAll(tourGuests);
        }

        public Model.Attendance Create(Model.Attendance tourGuest)
        {
            return _tourGuests.Save(tourGuest);
        }
    }
}
