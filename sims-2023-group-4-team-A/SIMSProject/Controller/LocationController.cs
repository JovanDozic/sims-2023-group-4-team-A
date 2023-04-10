using System.Collections.Generic;
using SIMSProject.Domain.Models;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class LocationController
    {
        private readonly LocationDAO _locations;
        public Location Location;

        public LocationController()
        {
            _locations = new LocationDAO();
            Location = new Location();
        }

        public List<Location> GetAll()
        {
            return _locations.GetAll();
        }

        public void SaveAll(List<Location> locations)
        {
            _locations.SaveAll(locations);
        }

        public Location Create(Location location)
        {
            return _locations.Save(location);
        }

        public Location GetByID(int id)
        {
            return _locations.Get(id);
        }
    }
}