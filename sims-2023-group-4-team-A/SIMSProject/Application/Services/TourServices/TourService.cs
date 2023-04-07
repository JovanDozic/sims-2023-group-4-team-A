using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application1.Services.TourServices
{
    public class TourService
    {
        private readonly TourRepository _repository;

        public TourService()
        {
            _repository = new();
        }

        public List<Tour> GetTours()
        {
            return _repository.GetAll();
        }

        public List<Tour> GetTodaysTours()
        {
            return _repository.FindTodaysTours();
        }


        public void CreateTour(Tour tour)
        {
            _repository.Save(tour);
        }

        public KeyPoint GoToNextKeyPoint(TourAppointment appointment)
        {
            var currentTour = _repository.GetById(appointment.TourId);
            if (currentTour == null)
            {
                return null;
            }

            var currentIndex = currentTour.KeyPoints.FindIndex(x => x.Id == appointment.CurrentKeyPointId);
            var indexOutOfRange = currentIndex < 0 || currentIndex >= currentTour.KeyPoints.Count - 1;

            if (indexOutOfRange)
            {
                return null;
            }

            return currentTour.KeyPoints[currentIndex + 1];
        }

        public KeyPoint GetLastKeyPoint(TourAppointment appointment)
        {
            return _repository.GetById(appointment.TourId)?.KeyPoints.Last();
        }

        public void EndTourAppointment(int tourId, int appointmentId)
        {
            var toEnd = _repository.GetById(tourId);
            if (toEnd == null)
            {
                return;
            }

            TourAppointment? appointmentToEnd = toEnd.Appointments.Find(x => x.Id == appointmentId);
            if (appointmentToEnd == null) return;
            appointmentToEnd.TourStatus = Status.COMPLETED;
            _repository.SaveAll(_repository.GetAll());
        }
    }
}
