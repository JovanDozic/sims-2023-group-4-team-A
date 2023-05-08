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

namespace SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels
{
    public class TourLiveTrackViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourGuestService _tourGuestService;
        private readonly NotificationService _notificationService;

        public BaseAppointmentViewModel Appointment { get; set; }
        public ObservableCollection<TourGuest> Guests { get; set; }

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
                }
            }
        }
        public string KeyPoints { get => Appointment.Tour.KeyPointsToString(); }
        public TourLiveTrackViewModel(TourAppointment appointment)
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            _tourService = Injector.GetService<TourService>();
            _notificationService = Injector.GetService<NotificationService>();

            Appointment = new(appointment);
            Guests = new(_tourGuestService.GetGuests(Appointment.TourAppointment));

            GoNextCommand = new RelayCommand(GoNextExecute, GoNextCanExecute);
            EndCommand = new RelayCommand(EndExecute, EndCanExecute);
            PauseCommand = new RelayCommand(PauseExecute, PauseCanExecute);
            SignUpCommand = new RelayCommand(SignUpExecute, SignUpCanExecute);
        }

        #region GoNextCommand
        public ICommand GoNextCommand { get; set; }
        public bool GoNextCanExecute()
        {
            bool reachedLast = Appointment.Tour.KeyPoints.Last().Id == Appointment.CurrentKeyPoint.Id;
            return !reachedLast;
        }
        public void GoNextExecute()
        {
            KeyPoint Next = _tourService.GetNextKeyPoint(Appointment.TourAppointment);
            Appointment.TourAppointment = _tourAppointmentService.AdvanceNext(Appointment.Id, Next);
            Appointment.CurrentKeyPoint = Next;
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
            return SelectedGuest.GuestStatus == GuestAttendance.ABSENT && Guests.Count > 0;
        }
        public void SignUpExecute()
        {
            _tourGuestService.SignUpGuest(SelectedGuest.Guest.Id, Appointment.Id);
            SendNotification();
        }
        private void SendNotification()
        {
            string title = "Potvrda prisustva";
            string description = $"{Appointment.TourAppointment.Guide} želi da potvrdi vaše prisustvo na turi:{Appointment.TourAppointment.Date}";

            var notification = new Notification(SelectedGuest.Guest, title, description, null);
            _notificationService.CreateNotification(notification);
        }
        #endregion
    }
}
