using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;
using System;
using SIMSProject.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Application.DTOs;
using System.Windows.Input;
using System.Windows.Controls;
using SIMSProject.Application.DTOs.TourDTOs;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class ToursViewModel: ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourGuestService _tourGuestService;
        public ObservableCollection<Tour> Tours { get; set; }

        private Tour _tour = new();
        public Tour SelectedTour
        {   
            get => _tour;
            set
            {
                if(value != _tour)
                {
                    _tour = value;
                    GuestAgeGroups = _ageGroupDictionary.GetValueOrDefault(_tour.Id);
                    VoucherUsage = _voucherDictionary.GetValueOrDefault(_tour.Id);
                    OnPropertyChanged(nameof(SelectedTour));
                }
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
                if(value == _guestAgeGroups) return;
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
                if(value == _voucherUsage) return;
                _voucherUsage = value;
                OnPropertyChanged(nameof(VoucherUsage));
            }
        }




        private Dictionary<int, GuestAgeGroupsDTO> _ageGroupDictionary { get; set; }
        private Dictionary<int, VoucherUsageDTO> _voucherDictionary { get; set; }

       
        public void GetFinishedTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetToursWithFinishedAppointments());
            _ageGroupDictionary = _tourService.MapToursGuestAgeGroups();
            _voucherDictionary = _tourService.MapToursVoucherUsage();
        }

        public void GetTodaysTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetTodaysTours());
        }


        public void GetStatistics(int? desiredYear = null)
        {
            TourStatistics = _tourService.GetMostVisitedTour(desiredYear);
        }

        public void Search(string locationAndLanguage, int searchDuration, int searchMaxGuests)
        {
           _tourService.SearchTours(locationAndLanguage, searchDuration, searchMaxGuests, Tours);
        }

        public bool IsSelected()
        {
            return SelectedTour != null;
        }
        public List<TourGuest> GetPendingTourGuests(User user)
        {
            return _tourGuestService.GetAllPendingByUser(user);
        }
        public void ConfirmTourGuestAttendance(TourGuest tourGuest)
        {
            _tourGuestService.MakeGuestPresent(tourGuest);

        }

        /* Commands for statistics*/

        public ICommand GetStatisticsCommand { get; set; }

        private bool CanExecuteGetStatistics()
        {
            return true;
        }

        private void ExecuteGetStatistics()
        {
            GetStatistics();
        }

        public string DesiredYear { get; set; } = string.Empty;

        public ICommand GetStatisticsCommand1 { get; set; }

        private bool CanExecuteGetStatistics1()
        {
            return DesiredYear.Length > 0;
        }

        private void ExecuteGetStatistics1()
        {
            GetStatistics(int.Parse(DesiredYear));
        }




        public ToursViewModel()
        {
            _tourService = Injector.GetService<TourService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            Tours = new(_tourService.GetTours());


            /* Commands mapping*/
            GetStatisticsCommand = new RelayCommand(ExecuteGetStatistics, CanExecuteGetStatistics);
            GetStatisticsCommand1 = new RelayCommand(ExecuteGetStatistics1, CanExecuteGetStatistics1);

        }
    }
}
