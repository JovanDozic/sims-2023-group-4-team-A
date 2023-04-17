using SIMSProject.Application.DTOs;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class AppointmentRatingViewModel: ViewModelBase
    {
        private readonly GuideRatingService _service;
        private TourAppontmentRatingDTO _rating = new();
        public TourAppontmentRatingDTO Rating
        {
            get => _rating;
            set
            {
                if(value == _rating) return;
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public string QAs{ get => Rating.Rating.QAsToString(); }

        public AppointmentRatingViewModel(TourAppontmentRatingDTO rating)
        {
            Rating = rating;
            _service = Injector.GetService<GuideRatingService>();
            
        }

        public void ReportReview()
        {
            _service.ReportReview(Rating.Rating.Id);
        }
    }
}
