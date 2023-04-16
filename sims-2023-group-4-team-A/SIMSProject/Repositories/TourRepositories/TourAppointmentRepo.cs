﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.ITourRepos;
using SIMSProject.FileHandler;
using SIMSProject.FileHandler.UserFileHandler;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourAppointmentRepo: ITourAppointmentRepo
    {
        private readonly TourAppointmentFileHandler _fileHandler;
        private List<TourAppointment> _tourAppointments;
        private readonly IKeyPointRepo _keyPointRepo;
        private readonly IGuestRepo _guestRepo;
        private readonly ITourGuestRepo _tourGuestRepo;
        private readonly ITourRepo  _tourRepo;



        public TourAppointmentRepo(IKeyPointRepo keyPointRepo, IGuestRepo guestRepo, ITourGuestRepo tourGuestRepo, ITourRepo tourRepo)
        {
            _fileHandler = new();
            _tourAppointments = _fileHandler.Load();
            _keyPointRepo = keyPointRepo;
            _guestRepo = guestRepo;
            _tourGuestRepo = tourGuestRepo;
            _tourRepo = tourRepo;

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
            appointment.Tour.Id = tour.Id;
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
                MapGuests(appointment);
                MapTour(appointment);
            }
        }
        private void MapTour(TourAppointment appointment)
        {
            appointment.Tour = _tourRepo.GetById(appointment.Tour.Id) ?? throw new Exception("Error! No matching tour.") ;
        }
        
        private void MapGuests(TourAppointment appointment)
        {
            List<TourGuest> pairs = _tourGuestRepo.GetAll().FindAll(x => x.Appointment.Id == appointment.Id);
            foreach (var pair in pairs)
            {
                Guest? matchingGuest = _guestRepo.GetAll().Find(x => x.Id == pair.Guest.Id) ?? throw new SystemException("Error!No matching guest!");
                appointment.Guests.Add(matchingGuest);
            }
        }

        private void MapCurrentKeyPoint(TourAppointment appointment)
        {
            if (appointment.CurrentKeyPoint.Id == -1)
                return;

            appointment.CurrentKeyPoint = _keyPointRepo.GetAll().Find(x => x.Id == appointment.CurrentKeyPoint.Id) ?? throw new SystemException("Error!No matching key point!");
        }

        public List<TourAppointment> GetAllByTourId(int tourId)
        {
            return GetAll().FindAll(x => x.Tour.Id == tourId && DateTime.Compare(x.Date, DateTime.Now) > 0);
        }
        public List<TourAppointment> FindTodaysAppointmentsByTour(int tourId)
        {
            return _tourAppointments.FindAll(x => x.Tour.Id == tourId && (DateTime.Compare(x.Date.Date, DateTime.Now.Date) == 0 || x.TourStatus == Status.ACTIVE));
        }

        public List<TourAppointment> FindTodaysAppointments()
        {
            return _tourAppointments.FindAll(x => (DateTime.Compare(x.Date.Date, DateTime.Now.Date) == 0 || x.TourStatus == Status.ACTIVE));
        }
    }
}
