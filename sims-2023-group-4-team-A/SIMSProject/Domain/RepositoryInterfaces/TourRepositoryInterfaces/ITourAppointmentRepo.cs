using Microsoft.VisualBasic;
using SIMSProject.Domain.Models.TourModels;
using System;
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
        public List<TourAppointment> GetTodaysAppointmentsByTour(int TourId);
        public List<TourAppointment> GetTodaysAppointments();
        public List<TourAppointment> GetAllByTour(int tourId);
        public List<TourAppointment> GetAllInactive(int tourId);
        public TourAppointment GetActive();
        public List<DateTime> GetBusyDates();
        public List<Tour> GetToursWithFinishedAppointments();
        public List<Tour> GetTodaysTours();
        public List<TourAppointment> GetAllUpcoming(int guideId);
        public List<TourAppointment> GetSuperGuideEligible(int GuideId, Language language);
        public List<TourAppointment> FindOverlapped(DateTime currentDate, List<TourAppointment> scheduled, DateTime appointmentEnd);
    }

}
