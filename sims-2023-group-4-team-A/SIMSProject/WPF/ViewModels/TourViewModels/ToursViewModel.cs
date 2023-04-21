using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Application.DTOs;

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

        public ToursViewModel()
        {
            _tourService = Injector.GetService<TourService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            Tours = new(_tourService.GetTours());
        }

        public void GetTodaysTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetTodaysTours());
        }


        public void GetStatistics()
        {
            TourStatistics = _tourService.GetMostVisitedTour();
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




    }
}
