using SIMSProject.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Observer;
using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Models.TourModels;

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

        public List<Tour> FindTodaysTours()
        {
            return _tours.FindTodaysTours();
        }

        public KeyPoint GoToNextKeyPoint(TourAppointment Date)
        {
            return _tours.GoToNextKeyPoint(Date);
        }

        public KeyPoint GetLast(TourAppointment date)
        {
            return _tours.GetLastKeyPoint(date);
        }


        public void Refresh()
        {
            _tours.Refresh();
        }

        public void Subscribe(IObserver observer) 
        { 
            _tours.Subscribe(observer);
        }

    }
}
