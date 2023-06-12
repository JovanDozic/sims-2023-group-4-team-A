using Microsoft.TeamFoundation.Common;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.Guest2;
using SIMSProject.WPF.Views.OwnerViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerScheduleRenovationViewModel : ViewModelBase, IDataErrorInfo
    {
        private User _user;
        private App _app = (App)System.Windows.Application.Current;
        private OwnerScheduleRenovationView _view;
        private OwnerAccommodationDetails _detailsView;
        private AccommodationRenovationService _renovationService;
        private AccommodationRenovation _renovation = new();


        public ObservableCollection<DateRange> AvailableDates { get; set; } = new();
        public AccommodationRenovation Renovation
        {
            get => _renovation;
            set
            {
                if (value == _renovation) return;
                _renovation = value;
                OnPropertyChanged();
            }
        }
        public Accommodation Accommodation
        {
            get => _renovation.Accommodation;
            set
            {
                if (value == _renovation.Accommodation) return;
                _renovation.Accommodation = value;
                OnPropertyChanged();
            }
        }
        public string StartDate
        {
            get => _renovation.StartDate.ToString("dd.MM.yyyy");
            set
            {
                if (value == _renovation.StartDate.ToString("dd.MM.yyyy")) return;
                _renovation.StartDate = DateTime.Parse(value, CultureInfo.GetCultureInfo("sr-LATN"));
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public string EndDate
        {
            get => _renovation.EndDate.ToString("dd.MM.yyyy"); 
            set
            {
                if (value == _renovation.EndDate.ToString("dd.MM.yyyy")) return;
                _renovation.EndDate = DateTime.Parse(value, CultureInfo.GetCultureInfo("sr-LATN"));
                OnPropertyChanged(nameof(EndDate));
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public int NumberOfDays
        {
            get => _renovation.NumberOfDays;
            set
            {
                if (value == _renovation.NumberOfDays) return;
                _renovation.NumberOfDays = value;
                OnPropertyChanged();
            }
        }
        public DateRange? SelectedDateRange
        {
            get => new DateRange(_renovation.StartDate, _renovation.EndDate);
            set
            {
                if (value == new DateRange(_renovation.StartDate, _renovation.EndDate)) return;
                if (value is null) return;
                _renovation.StartDate = value.StartDate;
                _renovation.EndDate = value.EndDate;
                OnPropertyChanged();
            }
        }
        public int SelectedDateRangeIndex { get; set; } = -1;
        public string Description
        {
            get => _renovation.Description; set
            {
                if (value == _renovation.Description) return;
                _renovation.Description = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand FindDatesCommand { get; set; }
        public RelayCommand ScheduleRenovationCommand { get; set; } 
        public bool DatesFound { get; set; } = false;

        public OwnerScheduleRenovationViewModel(User user, Accommodation accommodation, OwnerScheduleRenovationView view, OwnerAccommodationDetails detailsView)
        {
            _user = user;
            _view = view;
            _detailsView = detailsView;
            Accommodation = accommodation;

            _renovationService = Injector.GetService<AccommodationRenovationService>();

            FindDatesCommand = new RelayCommand(FindDates, CanFindDates);
            ScheduleRenovationCommand = new RelayCommand(ScheduleRenovation, CanScheduleRenovation);

            SelectedDateRange = null;
        }


        public bool CanFindDates()
        {
            if (_renovation.EndDate < _renovation.StartDate) return false;
            return true;
        }

        public bool CanBtnFindDates { get => CanFindDates(); }

        private void FindDates()
        {
            AvailableDates.Clear();
            SelectedDateRangeIndex = -1;
            AvailableDates = new(_renovationService.GetAvailableDateRanges(Accommodation, _renovation.StartDate, _renovation.EndDate, NumberOfDays));
            OnPropertyChanged(nameof(AvailableDates));
            if (AvailableDates.Count == 0)
            {
                if (_app.CurrentLanguage == "en-US")
                    MessageBox.Show("There are no free appointments for the given conditions.", "Error!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                else 
                    MessageBox.Show("Nema slobodnih termina za zadate uslove.", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);

                DatesFound = false;
            }
            else DatesFound = true;
            OnPropertyChanged(nameof(DatesFound));
        }

        private void ScheduleRenovation()
        {
            if (_app.CurrentLanguage == "en-US")
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }
            else
            {
                if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }

            try
            {
                _renovationService.SaveRenovation(_renovation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            
            if (_app.CurrentLanguage == "en-US")
                MessageBox.Show($"Renovation in {Accommodation.Name} successfully scheduled.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            else 
                MessageBox.Show($"Renoviranje u {Accommodation.Name} uspešno zakazano.", "Uspeh!", MessageBoxButton.OK, MessageBoxImage.Information);
            _view.NavigationService?.GoBack();
            _detailsView.ReloadRenovations();
        }

        public bool CanScheduleRenovation()
        {
            if (IsRenovationValid) return true;
            return false;
        }

        public bool CanBtnScheduleRenovation
        {
            get
            {
                return CanScheduleRenovation();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string? error = null;
                string requiredMessage = "Obavezno polje";
                switch (columnName)
                {
                    case nameof(StartDate):
                        if (string.IsNullOrEmpty(StartDate)) error = requiredMessage;
                        if (_renovation.EndDate < _renovation.StartDate) error = "Datum početka mora biti pre datuma završetka";
                        break;
                    case nameof(EndDate):
                        if (string.IsNullOrEmpty(EndDate)) error = requiredMessage;
                        if (_renovation.EndDate < _renovation.StartDate) error = "Datum završetka mora biti posle datuma početka";
                        break;
                    case nameof(NumberOfDays):
                        if (string.IsNullOrEmpty(NumberOfDays.ToString())) error = requiredMessage;
                        break;
                    case nameof(SelectedDateRange):
                        if (SelectedDateRange is null) error = requiredMessage;
                        if (SelectedDateRangeIndex == -1) error = requiredMessage;
                        break;
                    case nameof(Description):
                        if (string.IsNullOrEmpty(Description)) error = requiredMessage;
                        else if (Description.Length < 20) error = "Opis mora biti duži od 20 karaktera";
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error => null;
        public bool IsRenovationValid
        {
            get
            {
                foreach (var property in new string[] {
                    nameof(StartDate),
                    nameof(EndDate),
                    nameof(NumberOfDays),
                    nameof(SelectedDateRange),
                    nameof(Description) 
                })
                {
                    if (this[property] != null) return false;
                }
                return true;
            }
        }

    }
}
