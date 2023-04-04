using SIMSProject.Domain.TourModels;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application1.UseCases.Services.TourServices
{
    public class TourService
    {
        private readonly TourRepository _repository;

        public TourService()
        {
            _repository = new();
        }

        public KeyPoint GoToNextKeyPoint(TourAppointment appointment)
        {
            var currentTour = _repository.FindById(appointment.TourId);
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
            return _repository.FindById(appointment.TourId)?.KeyPoints.Last();
        }

        public void EndTourAppointment(int tourId, int appointmentId)
        {
            var toEnd = _repository.FindById(tourId);
            if (toEnd == null)
            {
                return;
            }

            TourAppointment? appointmentToEnd = toEnd.Appointments.Find(x => x.Id == appointmentId);
            if (appointmentToEnd == null) return;
            appointmentToEnd.TourStatus = "Završena";
            _repository.SaveAll(_repository.GetAll());
        }

        public void AddNewAppointment(int tourId, TourAppointment appointment) //servis
        {
            Tour tour = _repository.FindById(tourId);
            if (tour == null) return;
            tour.Appointments.Add(appointment);
        }
    }
}
