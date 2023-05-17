using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerHomeViewModel : ViewModelBase
    {
        private readonly User _user;
        public readonly AccommodationViewModel _accommodationViewModel;
        private readonly AccommodationReservationViewModel _reservationViewModel;
        private readonly AccommodationRenovationViewModel _renovationViewModel;
        private readonly NotificationService _notificationService;
        private ObservableCollection<Accommodation> _accommodations = new();
        private ObservableCollection<AccommodationReservation> _selectedAccommodationReservations = new();
        private ObservableCollection<AccommodationRenovation> _selectedAccommodationRenovations = new();
        private string _notificationIconSource = string.Empty;

        public Accommodation SelectedAccommodation { get; set; } = new();
        public AccommodationReservation SelectedReservation { get; set; } = new();
        public AccommodationRenovation SelectedRenovation { get; set; } = new();
        public ObservableCollection<Accommodation> Accommodations
        {
            get { return _accommodations; }
            set
            {
                if (_accommodations == value) return;
                _accommodations = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AccommodationRenovation> SelectedAccommodationRenovations
        {
            get => _selectedAccommodationRenovations;
            set
            {
                if (value == _selectedAccommodationRenovations) return;
                _selectedAccommodationRenovations = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AccommodationReservation> SelectedAccommodationReservations
        {
            get { return _selectedAccommodationReservations; }
            set
            {
                if (_selectedAccommodationReservations == value) return;
                _selectedAccommodationReservations = value;
                OnPropertyChanged();
            }
        }
        public string NotificationIconSource
        {
            get => _notificationIconSource;
            set
            {
                if (_notificationIconSource == value) return;
                _notificationIconSource = value;
                OnPropertyChanged();
            }
        }



        public OwnerHomeViewModel(User user)
        {
            _user = user;
            _accommodationViewModel = new(_user);
            _reservationViewModel = new(_user);
            _renovationViewModel = new(_user);
            _renovationViewModel = new(_user);
            _notificationService = Injector.GetService<NotificationService>();

            LoadAccommodations();
            SetNotificationIcon();
        }

        public void LoadReservations()
        {
            if (SelectedAccommodation == null) return;
            SelectedAccommodationReservations = _reservationViewModel.LoadReservationsByAccommodation(SelectedAccommodation);
        }

        public void LoadRenovations()
        {
            if (SelectedAccommodation == null) return;
            SelectedAccommodationRenovations = _renovationViewModel.LoadRenovationsByAccommodation(SelectedAccommodation);
        }

        public void LoadAccommodations()
        {
            if (Accommodations == null) return;
            Accommodations = _accommodationViewModel.LoadAccommodationsByOwner();
        }

        public void SetNotificationIcon()
        {
            if (_notificationService.AnyUnreadNotifications(_user)) NotificationIconSource = "../../../Resources/Images/NotificationAlert.png";
            else NotificationIconSource = "../../../Resources/Images/NotificationAlertNoColor.png";
        }

        public bool IsGuestRatingEnabled()
        {
            if (SelectedReservation == null) return false;
            if (SelectedReservation.GuestRated || 
                DateTime.Now < SelectedReservation.EndDate || 
                DateTime.Now > SelectedReservation.EndDate.AddDays(Consts.GuestRatingDeadline))
            {
                return false;
            }
            return true;
        }

        public bool IsOwnerRatingEnabled()
        {
            if (SelectedReservation == null) return false;
            else if (!SelectedReservation.GuestRated && IsGuestRatingEnabled()) return false;
            else if (!SelectedReservation.OwnerRated) return false;
            return true;
        }

        public void CancelRenovation()
        {
            _renovationViewModel.CancelRenovation(SelectedRenovation);
        }

        public bool IsRenovationCancelationEnabled(AccommodationRenovation? renovation)
        {
            if (renovation is null) return false;
            if (renovation.IsCancelled) return false;
            if (DateTime.Now >= renovation.StartDate.AddDays(-Consts.RenovationCancellationDeadline)) return false;
            return true;
        }
    }
}
