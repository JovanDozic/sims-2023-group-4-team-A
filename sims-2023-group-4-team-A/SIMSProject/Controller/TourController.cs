using SIMSProject.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model;

namespace SIMSProject.Controller
{
    public class TourController
    {
        private TourDAO _tours;
        public Tour Tour;

        public TourController()
        {
            _tours = new();
            Tour = new();
        }

        public List<Tour> GetAll()
        {
            return _tours.GetAll();
        }

        public void SaveAll(List<Tour> tours)
        {
            _tours.SaveAll(tours);
        }

        public Tour Create(Tour tour)
        {
            return _tours.Save(tour);
        }

        public List<Tour> GetToursWithSameLocation(Tour selectedTour)
        {
            return _tours.GetToursWithSameLocation(selectedTour);
        }

    }
}
