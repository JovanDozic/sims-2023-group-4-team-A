using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class LocationController
    {
        private LocationDAO _tourLocations;
        public Location TourLocation;

        public LocationController()
        {
            _tourLocations = new();
            TourLocation = new();
        }

        public List<Location> GetAll()
        {
            return _tourLocations.GetAll();
        }

        public void SaveAll(List<Location> tourLocations)
        {
            _tourLocations.SaveAll(tourLocations);
        }

        public Location Create(Location tourLocation)
        {
            return _tourLocations.Save(tourLocation);
        }

        public Location GetByID(int id)
        {
            return _tourLocations.Get(id);
        }
    }
}
