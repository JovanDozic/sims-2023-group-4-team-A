using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
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
            foreach (var appointment in tourAppointments)
            {
                _repo.Save(appointment, tour);
            }
        }
        public List<TourAppointment> GetTodays(Tour tour)
        {
            return _repo.GetTodaysAppointmentsByTour(tour.Id);
        }
        public List<TourAppointment> GetAllByTour(int tourId)
        {
            return _repo.GetAllByTour(tourId);
        }
        public List<TourAppointment> GetAllInactive(int tourId)
        {
            return _repo.GetAllInactive(tourId);
        }
        public TourAppointment AdvanceNext(int appointmentId, KeyPoint nextKeyPoint)
        {
            TourAppointment? appointment = _repo.GetById(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            appointment.CurrentKeyPoint = nextKeyPoint;
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
            if (!IsCancelable(oldAppointment)) return false;
            oldAppointment.TourStatus = Status.CANCELED;
            _repo.SaveAll(_repo.GetAll());
            return true;
        }
        public TourAppointment GetActive()
        {
            return _repo.GetActive();
        }
        public TourAppointment Activate(TourAppointment appointment, Tour tour)
        {
            TourAppointment? oldAppointment = _repo.GetById(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");

            oldAppointment.TourStatus = Status.ACTIVE;
            oldAppointment.CurrentKeyPoint = tour.KeyPoints.First();
            _repo.SaveAll(_repo.GetAll());
            return oldAppointment;
        }
        public void StopLiveTracking(int appointmentId)
        {
            TourAppointment? toEnd = _repo.GetById(appointmentId) ?? throw new ArgumentException("Error!Can't find appointment!");
            toEnd.TourStatus = Status.COMPLETED;
            _repo.SaveAll(_repo.GetAll());
        }
        public void UpdateAvailableSpots(TourAppointment appointment)
        {
            TourAppointment? oldAppointment = _repo.GetById(appointment.Id) ?? throw new ArgumentException("Error!Can't find appointment!");
            oldAppointment.AvailableSpots = appointment.AvailableSpots;
            _repo.SaveAll(_repo.GetAll());
        }
        public List<DateTime> GetBusyDates()
        {
            return _repo.GetBusyDates();
        }
        public List<Tour> GetTodaysTours()
        {
            return _repo.GetTodaysTours();
        }
        public List<Tour> GetToursWithFinishedAppointments()
        {
            return _repo.GetToursWithFinishedAppointments();
        }
        public List<TourAppointment> GetSuperGuideEligible(int GuideId, Language language)
        {
            return _repo.GetSuperGuideEligible(GuideId, language);
        }
    }
}
