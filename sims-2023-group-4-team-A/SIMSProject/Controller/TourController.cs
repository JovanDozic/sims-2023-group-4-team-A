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

        public List<Tour> SearchLocations(string locationId)
        {
            return _tours.SearchLocations(locationId);
        }

        public List<Tour> SearchDurations(string duration)
        {
            return _tours.SearchDurations(duration);
        }

        public List<Tour> SearchLanguages(string language)
        {
            return _tours.SearchLanguages(language);
        }

        public List<Tour> SearchMaxGuest(string maxGuests)
        {
            return _tours.SearchMaxGuests(maxGuests);
        }

        public List<Tour> FindTodays()
        {
            return _tours.FindTodaysTours();
        }




        /*public static List<Tour> Search(string searchText)
        {
            return _tours.Search(searchText);
        }*/
    }
}
