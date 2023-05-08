using SIMSProject.Application.DTOs;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using System.Windows.Input;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ReviewsViewModels
{
    public class AppointmentRatingViewModel : ViewModelBase
    {
        private readonly GuideRatingService _service;

        private TourAppointmentRatingDTO _rating = new();
        public TourAppointmentRatingDTO Rating
        {
            get => _rating;
            set
            {
                if (value == _rating) return;
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        private bool _reportingEnabled;
        public bool ReportingEnabled
        {
            get => _reportingEnabled;
            set
            {
                if (_reportingEnabled == value) return;
                _reportingEnabled = value;
                OnPropertyChanged(nameof(ReportingEnabled));

                ReportButtonContent = ReportingEnabled ? "Prijavi\nCtrl+R" : "Recenzija\nprijavljena";
            }
        }

        private string _reportButtonContent = "Recenzija\nprijavljena";
        public string ReportButtonContent
        {
            get => _reportButtonContent;
            set
            {
                if (value == _reportButtonContent) return;
                _reportButtonContent = value;
                OnPropertyChanged(nameof(ReportButtonContent));
            }
        }

        public string RatingDate { get => Rating.Rating.RatingDateToString(); }
        public string QAs { get => Rating.Rating.QAsToString(); }

        public AppointmentRatingViewModel(TourAppointmentRatingDTO rating)
        {
            Rating = rating;
            ReportingEnabled = !Rating.Rating.Reported;

            _service = Injector.GetService<GuideRatingService>();

            ReportCommand = new RelayCommand(ReportExecuted, ReportCanExecute);
        }
        public ICommand ReportCommand { get; private set; }

        public void ReportExecuted()
        {
            Rating.Rating.Reported = true;
            _service.ReportReview(Rating.Rating.Id);
            ReportingEnabled = !Rating.Rating.Reported;
        }

        public bool ReportCanExecute()
        {
            return ReportingEnabled;
        }

    }
}
