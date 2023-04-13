using SIMSProject.Application1.Services.TourServices;
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
        private readonly TourService _tourService = new();
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; } = new();

        public ToursViewModel()
        {
            Tours = new(_tourService.GetTours());
        }

        public void GetTodaysTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetTodaysTours());
        }
    }
}
