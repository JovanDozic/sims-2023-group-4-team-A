using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels
{
    public class ToursManagerViewModel: ViewModelBase
    {
        private readonly TourService _tourService;

        public ObservableCollection<Tour>? Tours { get; set; }
        private Tour _tour = new();
        public Tour SelectedTour
        {
            get => _tour;
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        public void GetTours()
        {
            Tours = new(_tourService.GetTours());
        }

        private void GetTodaysTours()
        {
            Tours = new(_tourService.GetTodaysTours());
        }

        public ToursManagerViewModel(string callerId)
        {
            _tourService = Injector.GetService<TourService>();
            switch(callerId)
            {
                case "TodaysTours": GetTodaysTours();break;
                case "AllTours": GetTours();break;
            }
        }
    }
}
