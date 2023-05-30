using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMSProject.WPF.ViewModels.Guest1ViewModels
{
    public class AnywhereAnytimeViewModel: ViewModelBase
    {
        private User _user = new();
        private AccommodationService _accommodationService;
        private AccommodationReservationService _accommodationReservationService;
        private AccommodationReservation _accommodationReservation = new();
        private Accommodation _selectedAccommodation = new();
        private DateTime _newDateBegin;
        private DateTime _newDateEnd;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<DateRange> DateRanges { get; set; } = new();
        public DateRange SelectedRange { get; set; } = new();
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value == _selectedAccommodation) return;
                _selectedAccommodation = value;
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
                    OnReservationDataChanged();
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
                OnReservationDataChanged();
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
                OnReservationDataChanged();
            }
        }
        public DateTime NewDateBegin
        {
            get => _newDateBegin;
            set
            {
                if (_newDateBegin == value) return;
                _newDateBegin = value;
                OnPropertyChanged();
            }
        }
        public DateTime NewDateEnd
        {
            get => _newDateEnd;
            set
            {
                if (_newDateEnd == value) return;
                _newDateEnd = value;
                OnPropertyChanged();
            }
        }
        public AnywhereAnytimeViewModel(User user)
        {
            _user = user;
            _accommodationService = Injector.GetService<AccommodationService>();
            _accommodationReservationService = Injector.GetService<AccommodationReservationService>();
            Accommodations = LoadAllAccommodations();
        }
        public ObservableCollection<Accommodation> LoadAllAccommodations()
        {
            return new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        }

        public void Search()
        {
            _accommodationService.SearchForFreeAccommodations(Accommodations, DateBegin, DateEnd, NumberOfDays, GuestsNumber);
        }

        private void OnReservationDataChanged()
        {
            if (DateBegin != null && DateEnd != null && NumberOfDays >= 1)
            {
                DateRanges = LoadDateRanges();
            }
        }

        public ObservableCollection<DateRange> LoadDateRanges()
        {
            var dates = new ObservableCollection<DateRange>();
            if(DateBegin != DateTime.MinValue && DateEnd != DateTime.MinValue)
            {
                for (DateTime date = DateBegin; date <= DateEnd.AddDays(-NumberOfDays); date = date.AddDays(1))
                {
                    DateTime endDateRange = date.AddDays(NumberOfDays);
                    DateRange dateRange = new DateRange(date, endDateRange);
                    dates.Add(dateRange);
                }
            }
            
            return dates;
        }

        public void SaveReservation()
        {
            _accommodationReservationService.SaveReservation(new AccommodationReservation(SelectedAccommodation.Id, _user.Id, SelectedRange.StartDate, SelectedRange.EndDate, NumberOfDays, GuestsNumber, false), _user);
            ToastNotificationService.ShowSuccess("Smeštaj uspešno rezervisan");
        }

        public void SaveReservationWithNewDates()
        {
            _accommodationReservationService.SaveReservation(new AccommodationReservation(SelectedAccommodation.Id, _user.Id, NewDateBegin, NewDateEnd, NumberOfDays, GuestsNumber, false), _user);
            ToastNotificationService.ShowSuccess("Smeštaj uspešno rezervisan");
        }

        public bool IsEndDateSelected ()
        {
            return DateBegin != DateTime.MinValue && DateEnd == DateTime.MinValue;

        }

        public bool IsStartDateSelected()
        {
            return DateEnd != DateTime.MinValue && DateBegin == DateTime.MinValue;
        }

        public bool IsDaysNumberValid()
        {
            if(DateBegin != DateTime.MinValue && DateEnd != DateTime.MinValue)
            {
                TimeSpan duration = DateEnd - DateBegin;
                return duration.Days < NumberOfDays;
            }
            return false;
        }

        public bool IsDateInPast()
        {
            if (DateBegin != DateTime.MinValue && DateEnd != DateTime.MinValue)
                return DateBegin >= DateEnd;
            else
                return false;
        }

        public bool AreDatesSelected()
        {
            return DateBegin != DateTime.MinValue && DateEnd != DateTime.MinValue;
        }
        public bool AreNewDatesSelected()
        {
            return NewDateBegin != DateTime.MinValue && NewDateEnd != DateTime.MinValue;
        }
        public void LoadFirstDatePicker(object sender)
        {
            if (sender is DatePicker datePicker)
            {
                datePicker.DisplayDateStart = DateTime.Today.AddDays(1);
            }
        }
        public void LoadSecondDatePicker(object sender)
        {
            if (sender is DatePicker datePicker)
            {
                datePicker.DisplayDateStart = DateTime.Today.AddDays(NumberOfDays + 1);
            }
        }
        public DateTime? GetUpdatedEndDate(DateTime? selectedStartDate)
        {
            if (selectedStartDate.HasValue && selectedStartDate.Value != DateTime.MinValue)
            {
                return selectedStartDate.Value.AddDays(NumberOfDays);
            }

            return null;
        }
        public DateTime? GetUpdatedStartDate(DateTime? selectedEndDate)
        {
            if (selectedEndDate.HasValue && selectedEndDate.Value != DateTime.MinValue)
            {
                return selectedEndDate.Value.AddDays(-NumberOfDays);
            }

            return null;
        }
    }
}
