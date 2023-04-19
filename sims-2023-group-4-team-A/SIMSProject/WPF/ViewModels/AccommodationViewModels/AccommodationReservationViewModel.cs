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
        private readonly ReschedulingRequestService _reservationRequestService;
        private readonly NotificationService _notificationService;
        private AccommodationReservation _selectedReservation = new();

        public ObservableCollection<AccommodationReservation> Reservations { get; set; } = new();
        public AccommodationReservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                if (value == _selectedReservation) return;
                _selectedReservation = value;
                OnPropertyChanged();
            }
        }

        public AccommodationReservationViewModel(User user)
        {
            _user = user;
            _reservationService = Injector.GetService<AccommodationReservationService>();
            _reservationRequestService = Injector.GetService<ReschedulingRequestService>();
            Reservations = LoadUnCanceledReservations();
            _notificationService = Injector.GetService<NotificationService>();
        }

        public void Update()
        {
            _reservationService.UpdateReservation(SelectedReservation);
        }

        public void SendNotification()
        {
            _notificationService.CreateNotification(AddNotification(SelectedReservation));
        }

        public Notification AddNotification(AccommodationReservation reservation) 
        {
            return new Notification(
                reservation.Accommodation.Owner,
                "Otkazana rezervacija",
                GetMessage(reservation));
        }

        public string GetMessage(AccommodationReservation reservation)
        {
            return $"Gost {reservation.Guest.Username} je otkazao rezervaciju {reservation.Accommodation.Name}({reservation.StartDate.ToString("dd.MM.yyyy.")} - {reservation.EndDate.ToString("dd.MM.yyyy.")})";
        }

        public void CancelReservation()
        {
            SelectedReservation.Canceled = true;
        }

        public bool IsReservationInPast()
        {
            return SelectedReservation.StartDate <= DateTime.Today;
        }

        public bool IsSelected()
        {
            return SelectedReservation != null;
            
        }

        public bool IsDateValid()
        {
            return DateTime.Today < SelectedReservation.StartDate.AddHours(-24);
               
        }

        public bool IsReservationOnStandBy()
        {
            return _reservationRequestService.CheckIfMatches(SelectedReservation);
        }

        public ObservableCollection<AccommodationReservation> LoadReservations()
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAll());
        }

        public ObservableCollection<AccommodationReservation> LoadUnCanceledReservations()
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAllUncancelled(_user));
        }

        public ObservableCollection<AccommodationReservation> LoadReservationsByAccommodation(Accommodation accommodation)
        {

            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAllByAccommodationId(accommodation.Id));
        }
    }
}
