using SIMSProject.Application.Services;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SIMSProject.WPF.ViewModels.TourViewModels.BaseViewModels;
using System.Windows.Input;
using SIMSProject.View.Guest2;
using System;
using SIMSProject.WPF.ViewModels.Messenger;
using SIMSProject.WPF.Messenger.Messages;

namespace SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels
{
    public class TourLiveTrackViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourGuestService _tourGuestService;
        private readonly NotificationService _notificationService;


        private KeyPoint _currentKeyPoint = new();
        public KeyPoint CurrentKeyPoint
        {
            get => _currentKeyPoint;
            set
            {
                if(value ==  _currentKeyPoint) return;
                _currentKeyPoint = value;
                OnPropertyChanged(nameof(CurrentKeyPoint));
            }
        }
        
        private TourAppointment _appointment = new();
        public TourAppointment Appointment
        {
            get => _appointment;
            set
            {
                if(value == _appointment) return;
                _appointment = value;
                OnPropertyChanged(nameof(Appointment));
            }
        }

        private ObservableCollection<TourGuest> _guests = new();
        public ObservableCollection<TourGuest> Guests
        {
            get => _guests;
            set
            {
                if (value == _guests) return;
                _guests = value;
                OnPropertyChanged(nameof(Guests));
            }
        }

        private TourGuest _selectedGuest = new();
        public TourGuest SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                if (value != _selectedGuest)
                {
                    _selectedGuest = value;
                    OnPropertyChanged(nameof(SelectedGuest));
                    OnPropertyChanged(nameof(Guests));
                }
            }
        }
        public TourLiveTrackViewModel()
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            _tourService = Injector.GetService<TourService>();
            _notificationService = Injector.GetService<NotificationService>();

            MessageBus.Subscribe<LiveTrackMessage>(this, OpenMessage);

            GoNextCommand = new RelayCommand(GoNextExecute, GoNextCanExecute);
            EndCommand = new RelayCommand(EndExecute, EndCanExecute);
            PauseCommand = new RelayCommand(PauseExecute, PauseCanExecute);
            SignUpCommand = new RelayCommand(SignUpExecute, SignUpCanExecute);
        }
        private void OpenMessage(LiveTrackMessage message)
        {
            Appointment = message.Appointment;
            CurrentKeyPoint = message.Appointment.CurrentKeyPoint;
            Guests = new(_tourGuestService.GetGuests(Appointment));
        }

        #region GoNextCommand
        public ICommand GoNextCommand { get; set; }
        public bool GoNextCanExecute()
        {
            bool reachedLast = Appointment.Id != 0 ? Appointment.Tour.KeyPoints.Last().Id == CurrentKeyPoint.Id:true;
            return !reachedLast;
        }
        public void GoNextExecute()
        {
            KeyPoint Next = _tourService.GetNextKeyPoint(Appointment);
            Appointment = _tourAppointmentService.AdvanceNext(Appointment.Id, Next);
            CurrentKeyPoint = Next;
        }
        #endregion
        #region EndCommand
        public ICommand EndCommand { get; set; }

        public event EventHandler RequestClose;

        private void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
        public bool EndCanExecute()
        {
            return true;
        }

        public void EndExecute()
        {
            _tourAppointmentService.StopLiveTracking(Appointment.Id);
            OnRequestClose();
        }
        #endregion
        #region PauseCommand
        public ICommand PauseCommand { get; private set; }
        public bool PauseCanExecute()
        {
            return true;
        }
        public void PauseExecute()
        {
            OnRequestClose();
        }
        #endregion
        #region SignUpCommand
        public ICommand SignUpCommand { get; private set; }
        public bool SignUpCanExecute()
        {
            return (SelectedGuest != null ? SelectedGuest.GuestStatus == GuestAttendance.ABSENT : false) && Guests.Count > 0;
        }
        public void SignUpExecute()
        {
            SelectedGuest = _tourGuestService.SignUpGuest(SelectedGuest.Guest.Id, Appointment.Id);
            SendNotification();
        }
        private void SendNotification()
        {
            string title = "Potvrda prisustva";
            string description = $"{Appointment.Guide} želi da potvrdi vaše prisustvo na turi:{Appointment.Date}";

            var notification = new Notification(SelectedGuest.Guest, title, description, null);
            _notificationService.CreateNotification(notification);

            Guests.Clear();
            foreach (var guests in _tourGuestService.GetGuests(Appointment))
            {
                Guests.Add(guests);
            }
        }
        #endregion
    }
}
