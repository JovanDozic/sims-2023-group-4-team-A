﻿using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.TourRepositoryInterfaces;
using SIMSProject.Repositories.TourRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Application.Services.TourServices
{
    public class TourGuestService
    {
        private readonly ITourGuestRepo _repo;

        public TourGuestService(ITourGuestRepo repo)
        {
            _repo = repo;
        }

        public TourGuest SignUpGuest(int guestId, int tourAppointmentId)
        {
            TourGuest? tourGuest = _repo.GetTourGuest(tourAppointmentId, guestId);
            if (tourGuest == null) return null;

            tourGuest.GuestStatus = GuestAttendance.PENDING;
            _repo.SaveAll(_repo.GetAll());
            return tourGuest;
        }

        public void MakeGuestPresent(TourGuest tourGuest)
        {
            tourGuest.JoiningPoint = tourGuest.TourAppointment.CurrentKeyPoint;
            tourGuest.GuestStatus = GuestAttendance.PRESENT;
            _repo.SaveAll(_repo.GetAll());
        }

        public List<TourGuest> GetGuests(TourAppointment appointment)
        {
            return _repo.GetGuests(appointment.Id);
        }
        public TourGuest GetTourGuest(TourAppointment appointment, int guestId)
        {
            return _repo.GetTourGuest(appointment.Id, guestId);
        }
        public TourGuest Save(TourGuest tourGuest)
        {
            return _repo.Save(tourGuest);
        }
        public List<TourGuest> GetAllPendingByUser(User user)
        {
            return _repo.GetAllPendingByUser(user);
        }
    }
}
