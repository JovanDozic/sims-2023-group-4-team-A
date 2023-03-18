using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class LocationController
    {
        private LocationDAO _locations;
        public Location Location;

        public LocationController()
        {
            _locations = new();
            Location = new();
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
