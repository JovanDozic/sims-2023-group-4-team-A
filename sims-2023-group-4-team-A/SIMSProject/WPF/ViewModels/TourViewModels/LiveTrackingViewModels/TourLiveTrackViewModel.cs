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
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourGuestService _tourGuestService;
        private readonly TourService _tourService;
        private readonly NotificationService _notificationService;

        public BaseTourViewModel Tour { get; set; }
        public BaseAppointmentViewModel Appointment { get; set; }
        public ObservableCollection<TourGuest> Guests { get; set; } = new();

        private ObservableCollection<TourAppointment> _appointments = new();
        public ObservableCollection<TourAppointment> Appointments
        {
            get => _appointments;
            set
            {
                if (_appointments == value) return;
                _appointments = value;
                OnPropertyChanged();
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
                }
            }
        }
        private TourAppointment _selectedAppointment = new();
        public TourAppointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (value != _selectedAppointment)
                {
                    _selectedAppointment = value;
                    Appointment = new(_selectedAppointment);
                    OnPropertyChanged(nameof(SelectedAppointment));
                }
            }
        }
        public string KeyPoints { get => Tour.KeyPointsToString(); }

        public TourLiveTrackViewModel(Tour tour)
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            _tourService = Injector.GetService<TourService>();
            _notificationService = Injector.GetService<NotificationService>();

            Tour = new(tour);
            Appointments = new(_tourAppointmentService.GetAllByTourId(tour.Id));

            GoNextCommand = new RelayCommand(GoNextExecute, GoNextCanExecute);
            EndCommand = new RelayCommand(EndExecute, EndCanExecute);
            PauseCommand = new RelayCommand(PauseExecute, PauseCanExecute);
            SignUpCommand = new RelayCommand(SignUpExecute, SignUpCanExecute);
        }

        public void AddGuests()
        {
            Guests = new(_tourGuestService.GetGuests(SelectedAppointment));
        }
        public void StartIfActivated()
        {
            TourAppointment? active = _tourAppointmentService.GetActiveByTour(Tour.Tour);
            if (active == null)
            {
                SelectedAppointment = _tourAppointmentService.Activate(SelectedAppointment, Tour.Tour);
                return;
            }
            else if (active.Id != SelectedAppointment.Id)
            {
                MessageBox.Show("Već postoji aktivna tura!");
                return;
            }
            SelectedAppointment = active;
        }

        #region GoNextCommand
        public ICommand GoNextCommand { get; set; }
        public bool GoNextCanExecute()
        {
            bool reachedLast = Tour.Tour.KeyPoints.Last().Id == Appointment.CurrentKeyPoint.Id;
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
            return SelectedGuest.GuestStatus == GuestAttendance.ABSENT;
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
