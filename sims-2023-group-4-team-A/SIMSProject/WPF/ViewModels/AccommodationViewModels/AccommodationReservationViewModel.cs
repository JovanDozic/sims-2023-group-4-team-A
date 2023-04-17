﻿using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationReservationViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly AccommodationReservationService _reservationService;
        private readonly ReschedulingRequestService _reservationRequestService;
        public ObservableCollection<AccommodationReservation> Reservations { get; set; } = new();
        private AccommodationReservation _selectedReservation;
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

        public void Update()
        {
            _reservationService.UpdateReservation(SelectedReservation);
        }

        public AccommodationReservation GetSelectedReservation()
        {
            return SelectedReservation;
        }
        public AccommodationReservationViewModel(User user)
        {
            _user = user;
            _reservationService = Injector.GetService<AccommodationReservationService>();
            _reservationRequestService = Injector.GetService<ReschedulingRequestService>();
            Reservations = LoadNotCanceledReservations();
        }

        public string GetMessage()
        {
            return $"Gost {SelectedReservation.Guest.Username} je otkazao rezervaciju {SelectedReservation.Accommodation.Name}({SelectedReservation.StartDate.ToString("dd.MM.yyyy.")} - {SelectedReservation.EndDate.ToString("dd.MM.yyyy.")})";
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
        public ObservableCollection<AccommodationReservation> LoadNotCanceledReservations()
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAll().Where(r => !r.Canceled));
        }
        public ObservableCollection<AccommodationReservation> LoadReservationsByAccommodation(Accommodation accommodation)
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAllByAccommodationId(accommodation.Id));
        }

    }
}
