using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;

namespace SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces
{
    public interface ITourAppointmentRepo
    {
        public int NextId();
        public List<TourAppointment> GetAll();
        public TourAppointment GetById(int id);
        public TourAppointment Save(TourAppointment appointment, Tour tour);
        public void SaveAll(List<TourAppointment> appointments);
        public List<TourAppointment> GetAllByTourId(int tourId);
        public List<TourAppointment> GetTodaysAppointmentsByTour(int TourId);
        public List<TourAppointment> GedTodaysAppointments();
    }
}
