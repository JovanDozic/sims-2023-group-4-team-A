﻿using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Windows;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Model;
using System.Collections.ObjectModel;
using SIMSProject.Domain.Models;

namespace SIMSProject.WPF.ViewModels.Guest1ViewModels
{
    public class ReservationViewModel: ViewModelBase
    {
        private readonly User _user = new();
        private readonly AccommodationReservationService _reservationService;
        private AccommodationViewModel _accommodationViewModel;
        private AccommodationReservationViewModel _accommodationReservationViewModel;
        public Accommodation SelectedAccommodation { get; set; } = new();
        public ObservableCollection<DateRange> DateRanges { get; set; } = new();
        public ObservableCollection<DateRange> AlternativeRanges { get; set; } = new();  
        public DateRange SelectedRange { get; set; } = new();
        
        public int GuestsNumber
        {
            get => _accommodationReservationViewModel.GuestsNumber;

            set
            {
                if (value != _accommodationReservationViewModel.GuestsNumber && value >= 1)
                {
                    _accommodationReservationViewModel.GuestsNumber = value;
                    OnPropertyChanged(nameof(GuestsNumber));
                }
            }
        }
        public int NumberOfDays
        {
            get => _accommodationReservationViewModel.NumberOfDays;

            set
            {
                if (value != _accommodationReservationViewModel.NumberOfDays && value >= 1)
                {
                    _accommodationReservationViewModel.NumberOfDays = value;
                    OnPropertyChanged(nameof(NumberOfDays));
                    OnReservationDataChanged();
                }
            }
        }
        public DateTime DateBegin
        {
            get => _accommodationReservationViewModel.DateBegin;
            set
            {
                if (_accommodationReservationViewModel.DateBegin == value) return;
                _accommodationReservationViewModel.DateBegin = value;
                OnPropertyChanged();
                OnReservationDataChanged();
            }
        }
        public DateTime DateEnd
        {
            get => _accommodationReservationViewModel.DateEnd;
            set
            {
                if (_accommodationReservationViewModel.DateEnd == value) return;
                _accommodationReservationViewModel.DateEnd = value;
                OnPropertyChanged();
                OnReservationDataChanged();
            }
        }
        public ReservationViewModel(AccommodationReservationViewModel accommodationReservationViewModel, AccommodationViewModel accommodationViewModel, User user)
        {
            _user = user;
            _accommodationViewModel = accommodationViewModel;
            _accommodationReservationViewModel = accommodationReservationViewModel;
            SelectedAccommodation = _accommodationViewModel.SelectedAccommodation;


            _reservationService = Injector.GetService<AccommodationReservationService>();
        }

        private void OnReservationDataChanged()
        {
            if (DateBegin != null && DateEnd != null && NumberOfDays >= 1)
            {
                DateRanges = LoadDateRanges();
                GetAvailableDateRange();
            }
        }
        public ObservableCollection<DateRange> LoadDateRanges()
        {
            var dates = new ObservableCollection<DateRange>();
            for (DateTime date = DateBegin; date <= DateEnd.AddDays(-NumberOfDays); date = date.AddDays(1))
            {
                DateTime endDateRange = date.AddDays(NumberOfDays);
                DateRange dateRange = new DateRange(date, endDateRange);
                dates.Add(dateRange);
            }
            return dates;
        }
        public void GetAvailableDateRange()
        {
            AlternativeRanges.Clear();
            DateTime today = DateTime.Today;
            DateTime startDate = DateBegin;
            DateTime endDate = DateEnd;

            int maxExtends = 7; //range extends max to 7 days
            int extendCount = 0;

            while (extendCount < maxExtends)
            {
                bool isAvailableBeforeConflict = CheckAvailability(startDate, startDate.AddDays(NumberOfDays));
                bool isAvailableAfterConflict = CheckAvailability(endDate.AddDays(-NumberOfDays), endDate);

                if (isAvailableBeforeConflict && isAvailableAfterConflict)
                {
                    AlternativeRanges.Add(new DateRange(startDate, startDate.AddDays(NumberOfDays)));
                    AlternativeRanges.Add(new DateRange(endDate.AddDays(-NumberOfDays), endDate));
                    break;
                }

                else if (isAvailableBeforeConflict)
                {
                    AlternativeRanges.Add(new DateRange(startDate, startDate.AddDays(NumberOfDays)));
                    break;
                }

                else if (isAvailableAfterConflict)
                {
                    AlternativeRanges.Add(new DateRange(endDate.AddDays(-NumberOfDays), endDate));
                    break;
                }

                if (startDate > today)
                {
                    startDate = startDate.AddDays(-1);
                }

                endDate = endDate.AddDays(1);
                extendCount++;
            }

        }
        public bool CheckAvailability(DateTime start, DateTime end)
        {
            foreach (var reservedDate in _accommodationViewModel.GetReservedDates())
            {
                if (start <= reservedDate.EndDate && reservedDate.StartDate <= end)
                {
                    return false;
                }
            }
            return true;
        }
        public void SaveReservation()
        {
            _reservationService.SaveReservation(new AccommodationReservation(_accommodationViewModel.SelectedAccommodation.Id, _user.Id, SelectedRange.StartDate, SelectedRange.EndDate, NumberOfDays, GuestsNumber, false),_user);
            ToastNotificationService.ShowSuccess("Smeštaj uspešno rezervisan");
        }

        public bool IsSelected()
        {
            return SelectedRange != null;
        }

    }
    
}
