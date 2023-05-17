using SIMSProject.Application.DTOs.TourDTOs;
using SIMSProject.Application.DTOs;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using SIMSProject.Domain.Injectors;

namespace SIMSProject.WPF.ViewModels.TourViewModels.StatisticsViewModels
{
    public class TourStatisticsViewModel: ViewModelBase
    {
        private readonly TourService _tourService;

        public ObservableCollection<Tour> Tours { get; set; } = new();

        private Tour _selectedTour = new();
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                if (value == _selectedTour) return;
                    _selectedTour = value;
                    GuestAgeGroups = AgeGroupDictionary.GetValueOrDefault(_selectedTour.Id) ?? throw new Exception("Error! No guest's age groups found");
                    VoucherUsage = VoucherDictionary.GetValueOrDefault(_selectedTour.Id) ?? throw new Exception("Error! No voucher's usage found");
                    OnPropertyChanged(nameof(SelectedTour));
            }
        }

        private TourStatisticsDTO _tourStatistics = new();
        public TourStatisticsDTO TourStatistics
        {
            get => _tourStatistics;
            set
            {
                if (value == _tourStatistics) return;
                _tourStatistics = value;
                OnPropertyChanged(nameof(TourStatistics));
            }
        }

        private GuestAgeGroupsDTO _guestAgeGroups = new();
        public GuestAgeGroupsDTO GuestAgeGroups
        {
            get => _guestAgeGroups;
            set
            {
                if (value == _guestAgeGroups) return;
                _guestAgeGroups = value;
                OnPropertyChanged(nameof(GuestAgeGroups));
            }
        }

        private VoucherUsageDTO _voucherUsage = new();
        public VoucherUsageDTO VoucherUsage
        {
            get => _voucherUsage;
            set
            {
                if (value == _voucherUsage) return;
                _voucherUsage = value;
                OnPropertyChanged(nameof(VoucherUsage));
            }
        }
        private Dictionary<int, GuestAgeGroupsDTO> AgeGroupDictionary { get; set; } = new();
        private Dictionary<int, VoucherUsageDTO> VoucherDictionary { get; set; } = new();
        
        private bool _rbAnnualyIsChecked;
        public bool RbAnnualyIsChecked
        {
            get => _rbAnnualyIsChecked;
            set
            {
                if (value != _rbAnnualyIsChecked)
                {
                    _rbAnnualyIsChecked = value;
                    OnPropertyChanged(nameof(RbAnnualyIsChecked));
                }
            }
        }

        private bool _rbYearIsChecked;
        public bool RbYearIsChecked
        {
            get => _rbYearIsChecked;
            set
            {
                if (value != _rbYearIsChecked)
                {
                    _rbYearIsChecked = value;
                    OnPropertyChanged(nameof(RbYearIsChecked));
                    TbYearIsEnabled = _rbYearIsChecked;
                }
            }
        }
        private bool _tbYearIsEnabled;
        public bool TbYearIsEnabled
        {
            get => _tbYearIsEnabled;
            set
            {
                if (_tbYearIsEnabled != value)
                {
                    _tbYearIsEnabled = value;
                    OnPropertyChanged(nameof(TbYearIsEnabled));
                }
            }
        }

        private string _desiredYear = String.Empty;
        public string DesiredYear
        {
            get => _desiredYear;
            set
            {
                if (value == _desiredYear) return;
                _desiredYear = value;
                OnPropertyChanged(nameof(DesiredYear));
            }
        }

        public void GetFinishedTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetToursWithFinishedAppointments());
        }

        #region GetStatisticsCommand
        public ICommand GetStatisticsCommand { get; set; }

        private bool CanExecuteGetStatistics()
        {
            return true;
        }

        private void ExecuteGetStatistics()
        {
            TourStatistics = _tourService.GetMostVisitedTour(null);
        }
        #endregion

        #region GetYearlyStatisticsCommand
        public ICommand GetYearlyStatisticsCommand { get; set; }

        private bool CanExecuteYearlyStatistics()
        {
            return !string.IsNullOrEmpty(DesiredYear);
        }

        private void ExecuteYearlyStatistics()
        {
            TourStatistics = _tourService.GetMostVisitedTour(int.Parse(DesiredYear));
        }
        #endregion

        public TourStatisticsViewModel()
        {
            _tourService = Injector.GetService<TourService>();

            AgeGroupDictionary = _tourService.MapToursGuestAgeGroups();
            VoucherDictionary = _tourService.MapToursVoucherUsage();

            GetStatisticsCommand = new RelayCommand(ExecuteGetStatistics, CanExecuteGetStatistics);
            GetYearlyStatisticsCommand = new RelayCommand(ExecuteYearlyStatistics, CanExecuteYearlyStatistics);
        }
    }
}
