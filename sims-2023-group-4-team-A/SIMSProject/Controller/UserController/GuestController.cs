using SIMSProject.Model.DAO.UserModelDAO;
using SIMSProject.Model.UserModel;
using System.Collections.Generic;

namespace SIMSProject.Controller.UserController
{
    public class GuestController
    {
        private GuestDAO _guests;
        public Guest Guest;

        public GuestController()
        {
            _guests = new();
            Guest = new();
        }

        public List<Guest> GetAll()
        {
            return _guests.GetAll();
        }

        public void SaveAll(List<Guest> guest)
        {
            _guests.SaveAll(guest);
        }

        public Guest Create(Guest guest)
        {
            return _guests.Save(guest);
        }

        public Guest GetByID(int id)
        {
            return _guests.Get(id);
        }

        public void RefreshRatings()
        {
            _guests.RefreshRatings();
        }
    }
}
