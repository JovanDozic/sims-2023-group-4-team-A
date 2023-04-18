using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.RepositoryInterfaces.AccommodationRepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepo _repo;
        private readonly NotificationService _notificationService;

        public AccommodationReservationService(IAccommodationReservationRepo repo)
        {
            _repo = repo;
            _notificationService = Injector.GetService<NotificationService>();

            UpdatePassedReservationNotifications();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _repo.GetAll();
        }

        public List<AccommodationReservation> GetAllByAccommodationId(int accommodationId)
        {
            return _repo.GetAllByAccommodationId(accommodationId);
        }

        private void UpdatePassedReservationNotifications()
        {
            var reservations = GetAll();
            foreach (var reservation in reservations)
            {
                if (reservation.EndDate < DateTime.Now && !reservation.RateGuestNotifSent)
                {
                    _notificationService.CreateNotification(PrepareNotification(reservation));
                    reservation.RateGuestNotifSent = true;
                }
            }
            _repo.SaveAll(reservations);
        }

        private Notification PrepareNotification(AccommodationReservation reservation)
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
    }
}
