﻿using SIMSProject.FileHandlers.UserFileHandler;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.Generic;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using SIMSProject.FileHandlers.TourFileHandlers;

namespace SIMSProject.Repositories.TourRepositories
{
    public class TourGuestRepo: ITourGuestRepo
    {
        private readonly TourGuestFileHandler _fileHandler;
        private List<TourGuest> _tourGuests;
        private readonly IKeyPointRepo _keyPointRepo;
        private readonly IGuest2Repo _guestRepo;
        private readonly ITourAppointmentRepo _tourAppointmentRepo;

        public TourGuestRepo(IKeyPointRepo keyPointRepo, IGuest2Repo guestRepo, ITourAppointmentRepo tourAppointmentRepo)
        {
            _fileHandler = new TourGuestFileHandler();
            _tourGuests = _fileHandler.Load();
            _keyPointRepo = keyPointRepo;
            _guestRepo = guestRepo;
            _tourAppointmentRepo = tourAppointmentRepo;

            MapTourGuests();
        }

        public List<TourGuest> GetAll()
        {
            return _tourGuests;
        }

        public TourGuest Save(TourGuest tourGuest)
        {
            TourGuest? old = GetTourGuest(tourGuest.TourAppointment.Id, tourGuest.Guest.Id);
            if (old != null) return null;

            _tourGuests.Add(tourGuest);
            _fileHandler.Save(_tourGuests);
            return tourGuest;
        }

        public void SaveAll(List<TourGuest> tourGuests)
        {
            _fileHandler.Save(tourGuests);
            _tourGuests = tourGuests;
        }

        private void MapTourGuests()
        {
            foreach (TourGuest tourGuest in _tourGuests)
            {
                MapKeyPoint(tourGuest);
                MapGuest(tourGuest);
                MapTourAppointment(tourGuest);
            }
        }

        private void MapTourAppointment(TourGuest tourGuest)
        {
            tourGuest.TourAppointment = _tourAppointmentRepo.GetAll().Find(x => x.Id == tourGuest.TourAppointment.Id) ?? throw new System.Exception("Error! No matching appointment.");
        }

        private  void MapGuest(TourGuest tourGuest)
        {
            tourGuest.Guest = _guestRepo.GetAll().Find(x => x.Id == tourGuest.Guest.Id) ?? throw new System.Exception("Error!No matching guest!");
        }

        private  void MapKeyPoint(TourGuest tourGuest)
        {
            if (tourGuest.JoiningPoint.Id == -1)
                return;

            tourGuest.JoiningPoint = _keyPointRepo.GetAll().Find(x => x.Id == tourGuest.JoiningPoint.Id) ?? throw new System.Exception("Error!No matching keyPoint!");
        }

        public List<TourGuest> GetGuests(int tourAppointmentId)
        {
            return _tourGuests.FindAll(x => x.TourAppointment.Id == tourAppointmentId);
        }

        public List<TourGuest> GetPresentGuests()
        {
            return _tourGuests.FindAll(x => x.GuestStatus == GuestAttendance.PRESENT);
        }

        public List<TourGuest> GetAllPendingByUser(User user)
        {
            return _tourGuests.FindAll(x => x.Guest.Id == user.Id && x.GuestStatus == GuestAttendance.PENDING);
        }

        public TourGuest GetTourGuest(int appointmentId, int guestId)
        {
            return _tourGuests.Find(x => x.TourAppointment.Id == appointmentId && x.Guest.Id == guestId);
        }
    }
}
