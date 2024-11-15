﻿using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views;
using SIMSProject.WPF.Views.Guest2Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourReservationsViewModel : ViewModelBase
    {
        #region Polja
        private readonly User _user;
        private readonly TourReservationService _reservationService;
        private readonly TourGuestService _tourGuestService;
        public ObservableCollection<TourReservation> _tourReservations = new();
        public ObservableCollection<TourReservation> TourReservations
        {
            get => _tourReservations;
            set
            {
                if (value == _tourReservations) return;
                _tourReservations = value;
                OnPropertyChanged();
            }
        }
        private TourReservation _selectedTourReservation = new();
        public TourReservation SelectedTourReservation
        {
            get => _selectedTourReservation;
            set
            {
                if (value == _selectedTourReservation) return;
                _selectedTourReservation = value;
                OnPropertyChanged(nameof(SelectedTourReservation));
                SetButtonsState();
            }
        }
        private TourReservation _tourReservation = new();
        public TourReservation TourReservation
        {
            get => _tourReservation;
            set
            {
                if (value == _tourReservation) return;
                _tourReservation = value;
                OnPropertyChanged(nameof(TourReservation));
            }
        }
        public DateTime Date
        {
            get => _selectedTourReservation.TourAppointment.Date;
            set
            {
                if (value != _selectedTourReservation.TourAppointment.Date)
                {
                    _selectedTourReservation.TourAppointment.Date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        private bool _isGuideRatingEnabled;
        public bool IsGuideRatingEnabled
        {
            get => _isGuideRatingEnabled;
            set
            {
                if(value == _isGuideRatingEnabled) return;
                _isGuideRatingEnabled = value;
                OnPropertyChanged(nameof(IsGuideRatingEnabled));
            }
        }
        private bool _isShowDetailsEnabled;
        public bool IsShowDetailsEnabled
        {
            get => _isShowDetailsEnabled;
            set
            {
                if (value == _isShowDetailsEnabled) return;
                _isShowDetailsEnabled = value;
                OnPropertyChanged(nameof(IsShowDetailsEnabled));
            }
        }
        public NavigationService NavService { get; set; }
        public RelayCommand ShowDetailsCommand { get; set; }
        public RelayCommand RateGuideCommand { get; set; }

        #endregion

        #region Konstruktori
        public TourReservationsViewModel(User user, NavigationService navigationService)
        {
            _user = user;
            NavService = navigationService;
            _reservationService = Injector.GetService<TourReservationService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            IsShowDetailsEnabled = false;
            IsGuideRatingEnabled = false;
            LoadReservationsByGuestId(_user.Id);

            ShowDetailsCommand = new RelayCommand(ShowDetailsCommandExecute, CanExecute_Command);
            RateGuideCommand = new RelayCommand(RateGuideCommandExecute, CanExecute_Command);         
        }
        #endregion

        #region Akcije
        private void ShowDetailsCommandExecute()
        {
            NavService.Navigate(new ShowKeyPoint(_user, SelectedTourReservation, NavService));
        }
        private void RateGuideCommandExecute()
        {
            NavService.Navigate(new RateGuide(_user, SelectedTourReservation, GetGuideId(), NavService));
            LoadReservationsByGuestId(_user.Id);
            SelectedTourReservation = null;
        }
        private bool CanExecute_Command()
        {
            return true;
        }
        public void LoadReservationsByGuestId(int GuestId)
        {
            TourReservations = new(_reservationService.GetAllByGuestId(GuestId));
        }

        public int GetGuideId()
        {
            return SelectedTourReservation.TourAppointment.Tour.Guide.Id;
        }
        public void SetButtonsState()
        {
            IsGuideRatingEnabled = IsRatingEnabled();
            IsShowDetailsEnabled = IsTourActive();
        }
        public bool IsRatingEnabled()
        {
            if (SelectedTourReservation == null) return false;
            var tourGuest = _tourGuestService.GetTourGuest(SelectedTourReservation.TourAppointment, _user.Id);
            if (SelectedTourReservation.TourAppointment.TourStatus == Status.COMPLETED && SelectedTourReservation.GuideRated == false && tourGuest.GuestStatus == GuestAttendance.PRESENT)
            {
                return true;
            }
            return false;
        }
        public bool IsTourActive()
        {
            if (SelectedTourReservation == null) return false;
            return true;
        }
#endregion
    }
}
