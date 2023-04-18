using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class ToursViewModel: ViewModelBase
    {
        private readonly TourService _tourService;
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

        public ToursViewModel()
        {
            _tourService = Injector.GetService<TourService>();
            Tours = new(_tourService.GetTours());
        }

        public void GetTodaysTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetTodaysTours());
        }

        public void Search(string locationAndLanguage, int searchDuration, int searchMaxGuests)
        {
           // _tourService.SearchTours(locationAndLanguage, searchDuration, searchMaxGuests, Tours);
        }

        public bool IsSelected()
        {
            return SelectedTour != null;
        }
    }
}
