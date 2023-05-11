using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationReservationViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly AccommodationReservationService _reservationService;
        private readonly ReschedulingRequestService _reservationRequestService;
        private readonly GuestRatingService _guestRatingService;
        private GuestRatingViewModel _guestRatingViewModel;
        private readonly NotificationService _notificationService;
        private AccommodationReservation _selectedReservation = new();
        private AccommodationReservation _accommodationReservation = new();

        public ObservableCollection<AccommodationReservation> Reservations { get; set; } = new();
        public ObservableCollection<DateRange> DateRanges { get; set; } = new();
        public ObservableCollection<DateRange> AlternativeRanges { get; set; } = new();
        public object AccommodationsCombo { get; private set; } = new();
        private double _overall;
        public double OverallRating
        {
            get { return _overall; }
            set { _overall = value; OnPropertyChanged(nameof(OverallRating)); }
        }

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
        public AccommodationReservation AccommodationReservation
        {
            get => _accommodationReservation;
            set
            {
                if (_accommodationReservation == value) return;
                _accommodationReservation = value;
                OnPropertyChanged();
            }
        }
        public int GuestsNumber
        {
            get => _accommodationReservation.GuestNumber;

            set
            {
                if (value != _accommodationReservation.GuestNumber && value >= 1)
                {
                    _accommodationReservation.GuestNumber = value;
                    OnPropertyChanged(nameof(GuestsNumber));
                }
            }
        }
        public int NumberOfDays
        {
            get => _accommodationReservation.NumberOfDays;

            set
            {
                if (value != _accommodationReservation.NumberOfDays && value >= 1)
                {
                    _accommodationReservation.NumberOfDays = value;
                    OnPropertyChanged(nameof(NumberOfDays));
                }
            }
        }
        public DateTime DateBegin
        {
            get => _accommodationReservation.StartDate;
            set
            {
                if (_accommodationReservation.StartDate == value) return;
                _accommodationReservation.StartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateEnd
        {
            get => _accommodationReservation.EndDate;
            set
            {
                if (_accommodationReservation.EndDate == value) return;
                _accommodationReservation.EndDate = value;
                OnPropertyChanged();
            }
        }

        public AccommodationReservationViewModel(User user)
        {
            _user = user;
            _reservationService = Injector.GetService<AccommodationReservationService>();
            _reservationRequestService = Injector.GetService<ReschedulingRequestService>();
            _guestRatingService = Injector.GetService<GuestRatingService>();
            _notificationService = Injector.GetService<NotificationService>();
            Reservations = LoadUnCanceledReservations();
            DateBegin = DateTime.Today.Date;
            DateEnd = DateTime.Today.Date.AddDays(1);
        }

        public void AddReservationsToCombo()
        {
            Reservations.Clear();
            foreach (var reservation in _reservationService.GetAll())
            {
                if (reservation.Guest.Id == _user.Id && reservation.OwnerRated && reservation.GuestRated && !reservation.Canceled)
                {
                    SetReservationDetails(reservation);
                    Reservations.Add(reservation);
                }
                AccommodationsCombo = Reservations;
            }
        }

        public void GetOverallRating()
        {
            if (SelectedReservation != null)
            {
                var rating = _guestRatingService.GetAll().FirstOrDefault(r => r.Reservation.Id == SelectedReservation.Id);
                if (rating != null)
                {
                    OverallRating = rating.Overall;
                }
            }
        }

        public void SetReservationDetails(AccommodationReservation reservation)
        {
            reservation.ReservationDetails = string.Format("{0} ({1} - {2})", reservation.Accommodation.Name, reservation.StartDate.ToShortDateString(), reservation.EndDate.ToShortDateString());
        }

        public void SaveReservation()
        {
            _reservationService.SaveReservation(SelectedReservation);
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
            return $"Gost {reservation.Guest.Username} je otkazao rezervaciju {reservation.Accommodation.Name}({reservation.StartDate:dd.MM.yyyy.} - {reservation.EndDate:dd.MM.yyyy.})";
        }

        public void CancelReservation()
        {
            SelectedReservation.Canceled = true;
        }

        public bool IsReservationInPast()
        {
            return SelectedReservation.StartDate <= DateTime.Today;
        }

        public bool IsNotSelected()
        {
            return SelectedReservation == null || SelectedReservation.StartDate == DateTime.MinValue || SelectedReservation.EndDate == DateTime.MinValue;
            
        }

        public bool IsDateValid()
        {
            return DateTime.Today < SelectedReservation.StartDate.AddHours(-24);
               
        }

        public bool IsReservationOnStandBy()
        {
            return _reservationRequestService.CheckIfMatches(SelectedReservation);
        }
   
        public List<DateRange> GetReservationDates(List<AccommodationReservation> reservations)
        {
            List<DateRange> dateRanges = new List<DateRange>();

            foreach (AccommodationReservation reservation in reservations)
            {
                dateRanges.Add(new DateRange(reservation.StartDate, reservation.EndDate));
            }
            return dateRanges;
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
