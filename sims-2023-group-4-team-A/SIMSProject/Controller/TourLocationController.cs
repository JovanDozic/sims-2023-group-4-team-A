using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Model.DAO;

namespace SIMSProject.Controller
{
    public class TourLocationController
    {
        private TourLocationDAO _tourLocations;
        public TourLocation TourLocation;

        public TourLocationController()
        {
            _tourLocations = new();
            TourLocation = new();
        }

        public List<TourLocation> GetAll()
        {
            return _tourLocations.GetAll();
        }

        public void SaveAll(List<TourLocation> tourLocations)
        {
            _tourLocations.SaveAll(tourLocations);
        }

        public TourLocation Create(TourLocation tourLocation)
        {
            return _tourLocations.Save(tourLocation);
        }

        public TourLocation GetByID(int id)
        {
            return _tourLocations.Get(id);
        }
    }
}
