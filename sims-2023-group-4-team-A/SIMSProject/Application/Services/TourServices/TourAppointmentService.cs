using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class TourAppointmentService
    {
        private readonly ITourAppointmentRepo _repo;

        public TourAppointmentService(ITourAppointmentRepo repo)
        {
            _repo = repo;
        }


        public void CreateAppointments(List<TourAppointment> tourAppointments, Tour tour)
        {
            foreach(var appointment in tourAppointments)
            {
               _repo.Save(appointment, tour);
            }
        }
        public List<TourAppointment> GetTodays(Tour tour)
        {
            return _repo.FindTodaysAppointmentsByTour(tour.Id);
        }
        public List<TourAppointment> GetAllByTourId(int tourId)
        {
            return _repo.GetAll().FindAll(x => x.TourId == tourId && DateTime.Compare(x.Date, DateTime.Now) > 0);
        }
        public List<TourAppointment> GetAllInactive(int tourId)
        {
            return GetAllByTourId(tourId).FindAll(x => x.TourStatus == Status.INACTIVE);
        }
        public TourAppointment GoToNextKeyPoint(int appointmentId, KeyPoint nextKeyPoint)
        {
            TourAppointment? appointment = _repo.GetById(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            appointment.CurrentKeyPoint = nextKeyPoint;
            appointment.CurrentKeyPointId = nextKeyPoint.Id;
            _repo.SaveAll(_repo.GetAll());
            return appointment;
        }

        private static bool IsCancelable(TourAppointment appointment)
        {
            return appointment.Date.AddHours(-48) > DateTime.Now;
        }

        public bool CancelAppointment(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _repo.GetById(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            if (!IsCancelable(oldAppointment))
            {
                return false;
            }

            oldAppointment.TourStatus = Status.CANCELED;
            _repo.SaveAll(_repo.GetAll());
            return true;
        }

        public TourAppointment GetActiveByTour(Tour tour)
        {
            return _repo.GetAll().Find(x => x.TourStatus == Status.ACTIVE && x.TourId == tour.Id);
        }

        public TourAppointment Activate(TourAppointment appointment, Tour tour)
        {
            TourAppointment? oldAppointment = _repo.GetById(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            oldAppointment.TourStatus = Status.ACTIVE;
            oldAppointment.CurrentKeyPoint = tour.KeyPoints.First();
            oldAppointment.CurrentKeyPointId = tour.KeyPoints.First().Id;
            _repo.SaveAll(_repo.GetAll());
            return oldAppointment;
        }

        public void StopLiveTracking(int appointmentId)
        {
            TourAppointment? toEnd = _repo.GetById(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            toEnd.TourStatus = Status.COMPLETED;
            _repo.SaveAll(_repo.GetAll());
        }

        public TourAppointment InitializeTour(TourAppointment appointment, Tour tour)
        {
            TourAppointment? oldAppointment = _repo.GetAll().Find(x => x.Id == appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");
            oldAppointment.Tour = tour;
            oldAppointment.TourId = tour.Id;
            oldAppointment.AvailableSpots = tour.MaxGuestNumber;
            _repo.SaveAll(_repo.GetAll());
            return oldAppointment;
        }

        public void UpdateAvailableSpots(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _repo.GetAll().Find(x => x.Id == appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");
            oldAppointment.AvailableSpots = appointment.AvailableSpots;
            _repo.SaveAll(_repo.GetAll());
        }
    }
}
