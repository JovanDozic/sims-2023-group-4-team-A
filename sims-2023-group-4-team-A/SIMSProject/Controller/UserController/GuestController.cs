using System.Collections.Generic;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model.DAO.UserModelDAO;

namespace SIMSProject.Controller.UserController
{
    public class GuestController
    {
        private readonly GuestDAO _guests;
        public Guest Guest;

        public GuestController()
        {
            _guests = new GuestDAO();
            Guest = new Guest();
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