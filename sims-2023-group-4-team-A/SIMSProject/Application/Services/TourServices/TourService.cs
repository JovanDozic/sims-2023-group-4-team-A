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
    public class TourService
    {
        private readonly ITourRepo _repo;
        private readonly ITourAppointmentRepo _appointmentRepo;

        public TourService(ITourRepo repo, ITourAppointmentRepo appointmentRepo)
        {
            _repo = repo;
            _appointmentRepo = appointmentRepo;
        }

        public List<Tour> GetTours()
        {
            return _repo.GetAll();
        }

        public List<Tour> GetTodaysTours()
        {
            List<TourAppointment> appointments = _appointmentRepo.FindTodaysAppointments();
            List<Tour> todays = new();
            foreach (var appointment in appointments)
            {
                if (todays.Any(x => x.Id == appointment.Tour.Id))
                    continue;
                todays.Add(appointment.Tour);
            }

            return todays;
        }


        public void CreateTour(Tour tour)
        {
            _repo.Save(tour);
        }

        public KeyPoint GetNextKeyPoint(TourAppointment appointment)
        {
            var currentTour = _repo.GetById(appointment.Tour.Id);
            if (currentTour == null)
            {
                return null;
            }

            var currentIndex = currentTour.KeyPoints.FindIndex(x => x.Id == appointment.CurrentKeyPoint.Id);
            var indexOutOfRange = currentIndex < 0 || currentIndex >= currentTour.KeyPoints.Count - 1;

            if (indexOutOfRange)
            {
                return null;
            }

            return currentTour.KeyPoints[currentIndex + 1];
        }

        public KeyPoint GetLastKeyPoint(TourAppointment appointment)
        {
            return _repo.GetById(appointment.Tour.Id)?.KeyPoints.Last();
        }

    }
}
