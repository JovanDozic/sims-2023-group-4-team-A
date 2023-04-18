using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationReservationViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly AccommodationReservationService _reservationService;
        private readonly NotificationService _notificationService;
        public ObservableCollection<AccommodationReservation> Reservations = new();

        public AccommodationReservationViewModel(User user)
        {
            _user = user;
            _reservationService = Injector.GetService<AccommodationReservationService>();
            _notificationService = Injector.GetService<NotificationService>();
        }

        

        public ObservableCollection<AccommodationReservation> LoadReservations()
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAll());
        }

        public ObservableCollection<AccommodationReservation> LoadReservationsByAccommodation(Accommodation accommodation)
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAllByAccommodationId(accommodation.Id));
        }

    }
}
