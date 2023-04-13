using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class ToursViewModel
    {
        private readonly TourService _tourService;
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; } = new();

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
    }
}
