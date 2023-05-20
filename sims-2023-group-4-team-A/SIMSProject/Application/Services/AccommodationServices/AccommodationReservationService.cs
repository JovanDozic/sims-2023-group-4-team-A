using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using SIMSProject.Domain.RepositoryInterfaces.UserRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Xceed.Wpf.AvalonDock.Converters;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepo _repo;
        private readonly IGuestRepo _guestRepo;
        private readonly NotificationService _notificationService;

        public AccommodationReservationService(IAccommodationReservationRepo repo, IGuestRepo guestRepo)
        {
            _repo = repo;
            _guestRepo = guestRepo;
            _notificationService = Injector.GetService<NotificationService>();

            UpdatePassedReservationNotifications();
        }

        public void SaveReservation(AccommodationReservation reservation, User user)
        {
            var guest = GetGuestByUser(user);
            if (guest.Role is UserRole.SuperGuest)
            {
                if (guest.BonusPoints > 0)
                {
                    guest.BonusPoints--;
                    _guestRepo.Update(guest);
                }
            }
            _repo.Save(reservation);
        }
        public List<AccommodationReservation> GetAll()
        {
            _repo.Load();
            return _repo.GetAll();
        }

        public List<AccommodationReservation> GetAllFromLastYear(User user)
        {
            return GetAllUncancelledByUser(user).FindAll(r => r.StartDate > DateTime.Now.AddYears(-1) && r.EndDate < DateTime.Now);
        }

        public List<AccommodationReservation> GetAllUncancelledByUser(User user)
        {
            return GetAll().Where(r => !r.Canceled && r.Guest.Id == user.Id).ToList();
        }

        public List<AccommodationReservation> GetAllUncancelled()
        {
            return GetAll().Where(r => !r.Canceled).ToList();
        }
        public List<AccommodationReservation> GetAllUncancelledFromFuture()
        {
            var futureReservations = GetAll();
            futureReservations.RemoveAll(x => x.StartDate <= DateTime.Now);
            return futureReservations;
        }

        public List<AccommodationReservation> GetAllByAccommodationId(int accommodationId)
        {
            return GetAll().FindAll(x => x.Accommodation.Id == accommodationId);
        }

        public List<AccommodationReservation> GetAllPastByAccommodationId(int accommodationId)
        {
            return GetAllByAccommodationId(accommodationId).FindAll(x => x.EndDate <= DateTime.Now);
        }

        public List<AccommodationReservation> GetAllUncanceledByAccommodationId(int accommodationId)
        {
            return GetAllByAccommodationId(accommodationId).FindAll(x => !x.Canceled);
        }

        public List<AccommodationReservation> GetAllFutureByAccommodationId(int accommodationId)
        {
            var futureReservations = GetAllByAccommodationId(accommodationId);
            futureReservations.RemoveAll(x => x.StartDate <= DateTime.Now);
            return futureReservations;
        }

        public void UpdateReservation(AccommodationReservation reservation)
        {
            _repo.Update(reservation);
        }

        public User UpdateGuestInfo(User user)
        {
            if (user is not Guest guest) return null;

            if (IsSuperGuest(guest) && (guest.AwardDate is null || DidDateExpire(guest.AwardDate)))
            {
                guest.Role = UserRole.SuperGuest;
                guest.BonusPoints = 5;
                guest.AwardDate = DateTime.Today;
            }
            else if(!IsSuperGuest(guest) && guest.AwardDate is null)
            {
                guest.Role = UserRole.Guest;
                guest.BonusPoints = 0;
                guest.AwardDate = null;
            }
            else if(!IsSuperGuest(guest) && DidDateExpire(guest.AwardDate))
            {
                guest.Role = UserRole.Guest;
                guest.BonusPoints = 0;
                guest.AwardDate = null;
            }
            _guestRepo.Update(guest);

            return guest;          
        }

        public bool DidDateExpire(DateTime? date)
        {
            return date.GetValueOrDefault().AddYears(1) < DateTime.Today;
        }

        public Guest GetGuestByUser(User user)
        {
            if(user is not Guest guest) return null;
            return guest;
        }

        public bool IsSuperGuest(User user)
        {
            if (user is not Guest guest) return false;
            return GetAllFromLastYear(user).Count >= 10;
        }

        private void UpdatePassedReservationNotifications()
        {
            foreach (var reservation in GetAll())
                if (reservation.EndDate < DateTime.Now && !reservation.RateGuestNotificationSent)
                {
                    _notificationService.CreateNotification(PrepareNotification(reservation));
                    reservation.RateGuestNotificationSent = true;
                    _repo.Update(reservation);
                }
        }

        private static Notification PrepareNotification(AccommodationReservation reservation)
        {
            return new Notification(
                reservation.Accommodation.Owner,
                Consts.RateGuestNotificationTitle,
                Consts.RateGuestNotificationDescription
                      .Replace("@guestUsername", reservation.Guest.Username)
                      .Replace("@endDate", reservation.EndDate.ToString("dd.MM.yyyy"))
                      .Replace("@accommodation", reservation.Accommodation.Name),
                reservation.EndDate.AddDays(Consts.GuestRatingDeadline)
            );
        }

        public List<DateRange> GetSchedule(Accommodation accommodation)
        {
            List<DateRange> schedule = new();
            foreach(var reservation in GetAllUncanceledByAccommodationId(accommodation.Id))
            {
                schedule.Add(new DateRange(reservation.StartDate, reservation.EndDate));
            }
            return schedule;
        }
    }
}
