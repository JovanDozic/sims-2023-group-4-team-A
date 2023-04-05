using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application1.Services.TourServices
{
    public class TourAppointmentService
    {
        private readonly TourAppointmentRepository _repository;

        public TourAppointmentService()
        {
            _repository = new();
        }

        public void GoToNextKeyPoint(int appointmentId, KeyPoint nextKeyPoint)
        {
            TourAppointment? appointment = _repository.Get(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            appointment.CurrentKeyPoint = nextKeyPoint;
            appointment.CurrentKeyPointId = nextKeyPoint.Id;
            _repository.SaveAll(_repository.GetAll());
        }

        private static bool IsCancelable(TourAppointment appointment)
        {
            return DateTime.Now.AddHours(-48) > appointment.Date;
        }

        public bool CancelAppointment(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _repository.Get(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            if (!IsCancelable(oldAppointment))
            {
                return false;
            }

            oldAppointment.TourStatus = Status.CANCELED;
            _repository.SaveAll(_repository.GetAll());
            return true;
        }

        public void StartLiveTracking(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _repository.Get(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            oldAppointment.TourStatus = Status.ACTIVE;
            oldAppointment.CurrentKeyPoint = appointment.CurrentKeyPoint;
            oldAppointment.CurrentKeyPointId = appointment.CurrentKeyPointId;
            _repository.SaveAll(_repository.GetAll());
        }

        public void StopLiveTracking(int appointmentId)
        {
            TourAppointment? toEnd = _repository.Get(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            toEnd.TourStatus = Status.COMPLETED;
            _repository.SaveAll(_repository.GetAll());
        }

        public TourAppointment InitializeTour(TourAppointment appointment, Tour tour)
        {
            TourAppointment? oldAppointment = _repository.GetAll().Find(x => x.Id == appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");
            oldAppointment.Tour = tour;
            oldAppointment.TourId = tour.Id;
            oldAppointment.AvailableSpots = tour.MaxGuestNumber;
            _repository.SaveAll(_repository.GetAll());
            return oldAppointment;
        }

        public void UpdateAvailableSpots(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _repository.GetAll().Find(x => x.Id == appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");
            oldAppointment.AvailableSpots = appointment.AvailableSpots;
            _repository.SaveAll(_repository.GetAll());
        }
    }
}
