using SIMSProject.Application.DTOs;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class TourRatingsViewModel: ViewModelBase
    {
        private readonly TourService  _tourService;
        
        
        private ObservableCollection<TourRatingDTO> _ratings = new();
        public ObservableCollection<TourRatingDTO> Ratings
        {
            get => _ratings;
            set
            {
                if(value == _ratings) return;
                _ratings = value;
                OnPropertyChanged(nameof(Ratings));
            }
        }

        private TourRatingDTO _selectedRating = new();
        public TourRatingDTO SelectedRating
        {
            get => _selectedRating;
            set
            {
                if(value == _selectedRating) return;
                _selectedRating = value;
                OnPropertyChanged(nameof(SelectedRating));
            }
        }

        private TourAppontmentRatingDTO _appointmentRating = new();
        public TourAppontmentRatingDTO AppointmentRating
        {
            get => _appointmentRating;
            set
            {
                if(value == _appointmentRating) return;
                _appointmentRating = value;
                OnPropertyChanged(nameof(AppointmentRating));
            }
        }

        public TourRatingsViewModel()
        {
            _tourService = Injector.GetService<TourService>();
            Ratings = new(_tourService.GetRatings());
        }
    }
}
