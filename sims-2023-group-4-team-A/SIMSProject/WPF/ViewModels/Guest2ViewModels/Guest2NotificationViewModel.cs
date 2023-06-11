using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class Guest2NotificationViewModel : ViewModelBase
    {
        #region Polja
        private User _user;
        private readonly NotificationService _service;
        private readonly TourGuestService _tourGuestService;
        private Notification _selectedNotification = new();
        private ObservableCollection<Notification> _notifications = new();
        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                if (_notifications == value) return;
                _notifications = value;
                OnPropertyChanged();
            }
        }
        public Notification SelectedNotification
        {
            get => _selectedNotification;
            set
            {
                if (_selectedNotification == value) return;
                _selectedNotification = value;
                OnPropertyChanged();
                SetButtonsState();
            }
        }
        private Visibility _yesButtonVisibility;
        public Visibility YesButtonVisibility
        {
            get => _yesButtonVisibility;
            set
            {
                if(_yesButtonVisibility == value) return;
                _yesButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _noButtonVisibility;
        public Visibility NoButtonVisibility
        {
            get => _noButtonVisibility;
            set
            {
                if (_noButtonVisibility == value) return;
                _noButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand ConfirmPresenceCommand { get; set; }
        public RelayCommand RejectPresenceCommand { get; set; }
        private bool _isYesEnabled;
        public bool IsYesEnabled
        {
            get => _isYesEnabled;
            set
            {
                if (value == _isYesEnabled) return;
                _isYesEnabled = value;
                OnPropertyChanged(nameof(IsYesEnabled));
            }
        }
        private bool _isNoEnabled;
        public bool IsNoEnabled
        {
            get => _isNoEnabled;
            set
            {
                if (value == _isNoEnabled) return;
                _isNoEnabled = value;
                OnPropertyChanged(nameof(IsNoEnabled));
            }
        }
        #endregion

        #region Konstruktori
        public Guest2NotificationViewModel(User user)
        {
            _user = user;
            _service = Injector.GetService<NotificationService>();
            _tourGuestService = Injector.GetService<TourGuestService>();

            Notifications = new(_service.GetAllUnreadByUser(_user));
            SetButtonsState();
            IsYesEnabled = true;
            IsNoEnabled = true;

            ConfirmPresenceCommand = new RelayCommand(ConfirmPresenceCommandExecute, CanExecute_Command);
            RejectPresenceCommand = new RelayCommand(RejectPresenceCommandExecute, CanExecute_Command);
        }
        #endregion

        #region Akcije
        public void SetButtonsState()
        {
            if (SelectedNotification?.Title == "Potvrda prisustva")
            {
                YesButtonVisibility = Visibility.Visible;
                NoButtonVisibility = Visibility.Visible;
                return;
            }
            YesButtonVisibility = Visibility.Hidden;
            NoButtonVisibility = Visibility.Hidden;
        }
        
        private void ConfirmPresenceCommandExecute()
        {
            IsNoEnabled = false;
            IsYesEnabled = false;
            foreach (var tourGuest in GetPendingTourGuests(_user))
            {
                
                ConfirmTourGuestAttendance(tourGuest);
                _service.MarkAsRead(SelectedNotification);
                
            }
        }
        private void RejectPresenceCommandExecute()
        {
            IsNoEnabled = false;
            IsYesEnabled = false;
            _service.MarkAsRead(SelectedNotification);
        }
        private bool CanExecute_Command()
        {
            return true;
        }
        public List<TourGuest> GetPendingTourGuests(User user)
        {
            return _tourGuestService.GetAllPendingByUser(user);
        }
        public void ConfirmTourGuestAttendance(TourGuest tourGuest)
        {
            _tourGuestService.MakeGuestPresent(tourGuest);
        }

        #endregion
    }
}
