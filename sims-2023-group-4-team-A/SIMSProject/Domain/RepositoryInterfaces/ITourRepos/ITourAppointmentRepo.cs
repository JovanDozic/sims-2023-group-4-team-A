using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.RepositoryInterfaces.ITourRepos
{
    public interface ITourAppointmentRepo
    {
        public int NextId();
        public List<TourAppointment> GetAll();
        public TourAppointment GetById(int id);
        public TourAppointment Save(TourAppointment appointment, Tour tour);
        public void SaveAll(List<TourAppointment> appointments);
        public List<TourAppointment> GetAllByTourId(int tourId);
        public List<TourAppointment> FindTodaysAppointmentsByTour(int TourId);
        public List<TourAppointment> FindTodaysAppointments();
    }
}
