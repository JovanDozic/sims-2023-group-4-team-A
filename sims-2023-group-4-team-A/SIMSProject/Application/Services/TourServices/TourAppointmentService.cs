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
        private readonly TourAppointmentRepo _repository;

        public TourAppointmentService()
        {
            _repository = new();
        }


        public void CreateAppointments(List<TourAppointment> tourAppointments, Tour tour)
        {
            foreach(var appointment in tourAppointments)
            {
               _repository.Save(appointment, tour);
            }
        }
        public List<TourAppointment> GetAllByTourId(int tourId)
        {
            return _repository.GetAll().FindAll(x => x.TourId == tourId && DateTime.Compare(x.Date, DateTime.Now) > 0);
        }

        public void GoToNextKeyPoint(int appointmentId, KeyPoint nextKeyPoint)
        {
            TourAppointment? appointment = _repository.GetById(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            appointment.CurrentKeyPoint = nextKeyPoint;
            appointment.CurrentKeyPointId = nextKeyPoint.Id;
            _repository.SaveAll(_repository.GetAll());
        }

        private static bool IsCancelable(TourAppointment appointment)
        {
            return appointment.Date.AddHours(-48) > DateTime.Now;
        }

        public bool CancelAppointment(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _repository.GetById(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

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
            TourAppointment? oldAppointment = _repository.GetById(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            oldAppointment.TourStatus = Status.ACTIVE;
            oldAppointment.CurrentKeyPoint = appointment.CurrentKeyPoint;
            oldAppointment.CurrentKeyPointId = appointment.CurrentKeyPointId;
            _repository.SaveAll(_repository.GetAll());
        }

        public void StopLiveTracking(int appointmentId)
        {
            TourAppointment? toEnd = _repository.GetById(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
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
