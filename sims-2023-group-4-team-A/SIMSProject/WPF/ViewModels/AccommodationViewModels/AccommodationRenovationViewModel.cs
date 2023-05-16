using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Channels;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationRenovationViewModel : ViewModelBase
    {
        private User _user;
        private readonly AccommodationRenovationService _renovationService;
        private AccommodationRenovation _renovation = new();
        private ObservableCollection<DateRange> _datesSource = new();

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
        public ObservableCollection<AccommodationRenovation> Renovations { get; set; } = new();

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
        public DateTime StartDate
        {
            get => _renovation.StartDate;
            set
            {
                if (value == _renovation.StartDate) return;
                _renovation.StartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDate
        {
            get => _renovation.EndDate; set
            {
                if (value == _renovation.EndDate) return;
                _renovation.EndDate = value;
                OnPropertyChanged();
            }
        }
        public DateRange SelectedDateRange { get; set; } = new();
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
        public string Description
        {
            get => _renovation.Description; set
            {
                if (value == _renovation.Description) return;
                _renovation.Description = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DateRange> DatesSource
        {
            get => _datesSource;
            set
            {
                if (value == _datesSource) return;
                _datesSource = value;
                OnPropertyChanged();
            }
        }


        public AccommodationRenovationViewModel(User user)
        {
            _user = user;
            _renovationService = Injector.GetService<AccommodationRenovationService>();

            LoadRenovations();
        }

        public void LoadRenovations()
        {
            Renovations = new(_renovationService.GetAll());
        }

        public ObservableCollection<AccommodationRenovation> LoadRenovationsByAccommodation(Accommodation accommodation)
        {
            return new ObservableCollection<AccommodationRenovation>(_renovationService.GetAllByAccommodationId(accommodation.Id));
        }

        public void FindDates()
        {
            if (!CanScheduleRenovation())
            {
                MessageBox.Show("Početak opsega ne može biti veći od kraja.", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DatesSource = new(_renovationService.GetAvailableDateRanges(Accommodation, StartDate, EndDate, NumberOfDays));
            if (DatesSource.Count == 0) MessageBox.Show("Nema slobodnih termina za zadate uslove.", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ScheduleRenovation()
        {
            try
            {
                StartDate = SelectedDateRange.StartDate;
                EndDate = SelectedDateRange.EndDate;
                _renovationService.SaveRenovation(Renovation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            MessageBox.Show($"Renoviranje u {Accommodation.Name} uspešno zakazano.", "Uspešno zakazivanje!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool CanScheduleRenovation()
        {
            return StartDate <= EndDate;
        }

        // Used only for OLD home view structure, TODO: remove after implementing HCI UI
        internal void CancelRenovation(AccommodationRenovation renovation)
        {
            _renovationService.CancelRenovation(renovation);
        }
    }
}
