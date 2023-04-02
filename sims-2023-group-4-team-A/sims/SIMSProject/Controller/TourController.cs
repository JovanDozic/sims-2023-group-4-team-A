using SIMSProject.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model;
using SIMSProject.Observer;

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

        public List<Tour> SearchMaxGuest(string maxGuests)
        {
            return _tours.SearchMaxGuests(maxGuests);
        }

        public List<Tour> FindTodays()
        {
            return _tours.FindTodaysTours();
        }

        public KeyPoint FindNext(TourDate Date)
        {
            return _tours.GetNextKeyPoint(Date);
        }

        public KeyPoint GetLast(TourDate date)
        {
            return _tours.FindLastKeyPoint(date);
        }

        public void EndTour(int tourId, int dateId)
        {
            _tours.EndTour(tourId, dateId);
        }

        public void AddNewDate(int tourId, TourDate date)
        {
            _tours.AddNewDate(tourId, date);
        }

        public void Refresh()
        {
            _tours.Refresh();
        }

        public void Subscribe(IObserver observer) 
        { 
            _tours.Subscribe(observer);
        }

        /*public static List<Tour> Search(string searchText)
        {
            return _tours.Search(searchText);
        }*/
    }
}
