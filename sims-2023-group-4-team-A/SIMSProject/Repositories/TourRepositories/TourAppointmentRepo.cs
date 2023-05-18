﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.FileHandlers.UserFileHandler;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using SIMSProject.FileHandlers.TourFileHandlers;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourAppointmentRepo : ITourAppointmentRepo
    {
        private readonly TourAppointmentFileHandler _fileHandler;
        private List<TourAppointment> _tourAppointments;
        private readonly IKeyPointRepo _keyPointRepo;
        private readonly IGuideRepo _guideRepo;
        private readonly ITourRepo _tourRepo;

        public TourAppointmentRepo(IKeyPointRepo keyPointRepo, ITourRepo tourRepo, IGuideRepo guideRepo)
        {
            _fileHandler = new();
            _tourAppointments = _fileHandler.Load();
            _keyPointRepo = keyPointRepo;
            _tourRepo = tourRepo;
            _guideRepo = guideRepo;

            MapAppointments();
        }

        public int NextId() { return _tourAppointments.Max(x => x.Id) + 1; }
        public List<TourAppointment> GetAll() { return _tourAppointments; }
        public TourAppointment GetById(int id)
        {
            return _tourAppointments.Find(x => x.Id == id);
        }

        public TourAppointment Save(TourAppointment appointment, Tour tour)
        {
            appointment.Id = NextId();
            appointment.Tour = tour;
            appointment.AvailableSpots = tour.MaxGuestNumber;
            _tourAppointments.Add(appointment);
            _fileHandler.Save(_tourAppointments);
            return appointment;
        }

        public void SaveAll(List<TourAppointment> appointments)
        {
            _fileHandler.Save(appointments);
            _tourAppointments = appointments;
        }

        private void MapAppointments()
        {
            foreach (TourAppointment appointment in _tourAppointments)
            {
                MapCurrentKeyPoint(appointment);
                MapTour(appointment);
                MapGuide(appointment);
            }
        }
        private void MapTour(TourAppointment appointment)
        {
            appointment.Tour = _tourRepo.GetById(appointment.Tour.Id) ?? throw new Exception("Error! No matching tour.");
        }

        private void MapGuide(TourAppointment appointment)
        {
            appointment.Guide = _guideRepo.GetById(appointment.Guide.Id) ?? throw new Exception("Error! No matching guide.");
        }
        private void MapCurrentKeyPoint(TourAppointment appointment)
        {
            if (appointment.CurrentKeyPoint.Id == -1)
                return;

            appointment.CurrentKeyPoint = _keyPointRepo.GetAll().Find(x => x.Id == appointment.CurrentKeyPoint.Id) ?? throw new SystemException("Error!No matching key point!");
        }

        public List<TourAppointment> GetAllByTourId(int tourId)
        {
            return _tourAppointments.FindAll(x => x.Tour.Id == tourId && DateTime.Compare(x.Date, DateTime.Now) > 0);
        }
        public List<TourAppointment> GetTodaysAppointmentsByTour(int tourId)
        {
            return _tourAppointments.FindAll(x => x.Tour.Id == tourId && (DateTime.Compare(x.Date.Date, DateTime.Now.Date) == 0 || x.TourStatus == Status.ACTIVE));
        }
        public List<TourAppointment> GetTodaysAppointments()
        {
            return _tourAppointments.FindAll(x => (DateTime.Compare(x.Date.Date, DateTime.Now.Date) == 0 || x.TourStatus == Status.ACTIVE));
        }

        public List<TourAppointment> GetAllByTour(int tourId)
        {
            return _tourAppointments.FindAll(x => x.Tour.Id == tourId && DateTime.Compare(x.Date, DateTime.Now) > 0);
        }

        public List<TourAppointment> GetAllInactive(int tourId)
        {
            return GetAllByTour(tourId).FindAll(x => x.TourStatus == Status.INACTIVE);
        }

        public TourAppointment GetActive()
        {
            return _tourAppointments.Find(x => x.TourStatus == Status.ACTIVE);

        }
        public List<DateTime> GetBusyDates()
        {
            return _tourAppointments.Select(x => x.Date).Distinct().ToList();

        }

        public List<Tour> GetToursWithFinishedAppointments()
        {
            return _tourAppointments
            .Where(x => x.TourStatus == Status.COMPLETED)
            .Select(x => x.Tour).Distinct().ToList();
        }

        public List<Tour> GetTodaysTours()
        {
            return GetTodaysAppointments().Select(x => x.Tour).Distinct().ToList();
        }
    }
}
