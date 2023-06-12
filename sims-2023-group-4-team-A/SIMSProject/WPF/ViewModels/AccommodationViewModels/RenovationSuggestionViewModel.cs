using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using System.Collections.Generic;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class RenovationSuggestionViewModel: ViewModelBase
    {
        private RenovationSuggestion _renovation = new();
        private OwnerRating _rating = new();
        private List<string> _levels = new();
        private RenovationSuggestionService _renovationService;
        private OwnerRatingService _ratingService;
        public RenovationSuggestion Renovation
        {
            get => _renovation;
            set
            {
                if (_renovation == value) return;
                _renovation = value;
                OnPropertyChanged();
            }
        }
        public AccommodationReservation SelectedReservation
        {
            get => _rating.Reservation;
            set
            {
                if (value == _rating.Reservation) return;
                _rating.Reservation = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get => _renovation.Comment;
            set
            {
                if (_renovation.Comment == value) return;
                _renovation.Comment = value;
                OnPropertyChanged();
            }
        }
        public string Level
        {
            get => _renovation.LevelOfEmergency;
            set
            {
                if (_renovation.LevelOfEmergency == value) return;
                _renovation.LevelOfEmergency = value;
                OnPropertyChanged();
            }
        }
        public List<string> Levels
         {
            get => _levels;
            set
            {
                _levels = value;
                OnPropertyChanged();
            }       
         }
        public OwnerRating Rating
        {
            get => _rating;
            set
            {
                if (_rating == value) return;
                _rating = value;
                OnPropertyChanged();
            }
        }

        public RenovationSuggestionViewModel(OwnerRating rating)
        {
            Rating = rating;
            _ratingService = Injector.GetService<OwnerRatingService>();
            _renovationService = Injector.GetService<RenovationSuggestionService>();
            Levels = new List<string>
            {
            "Nivo 1 - bilo bi lepo renovirati neke sitnice, ali sve funkcioniše kako treba i bez toga",
            "Nivo 2 - male zamerke na smeštaj koje kada bi se uklonile bi ga učinile savršenim",
            "Nivo 3 - nekoliko stvari koje su baš zasmetale bi trebalo renovirati",
            "Nivo 4 - ima dosta loših stvari i renoviranje je stvarno neophodno",
            "Nivo 5 - smeštaj je u jako lošem stanju i ne vredi ga uopšte iznajmljivati ukoliko se ne renovira"
        };
        }

        public void SendRequest()
        {
            _renovationService.SendRequest(_renovation);
        }
        public void RateWithRenovation(RenovationSuggestion renovation)
        {
            SelectedReservation.OwnerRated = true;
            _rating.RenovationSuggestion = renovation;
            _ratingService.LeaveRating(_rating);
            ToastNotificationService.ShowSuccess("Ocena i preporuka uspešno poslati");
        }
    }
}
