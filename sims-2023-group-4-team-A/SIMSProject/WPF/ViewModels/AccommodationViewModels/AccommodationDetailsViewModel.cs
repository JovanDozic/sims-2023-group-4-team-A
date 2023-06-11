using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationDetailsViewModel : ViewModelBase
    {
        private readonly User _user;
        private Accommodation _accommodation = new();
        private AccommodationStatistic _statistic = new();
        private ObservableCollection<AccommodationReservation> _reservations = new();
        private ObservableCollection<AccommodationRenovation> _renovations = new();
        private readonly AccommodationReservationService _reservationService;
        private readonly AccommodationStatisticService _statisticService;
        private readonly OwnerRatingService _ratingService;
        private readonly AccommodationRenovationService _renovationService;
        
        public Accommodation Accommodation
        {
            get => _accommodation;
            set
            {
                if (_accommodation == value) return;
                _accommodation = value;
                OnPropertyChanged();
            }
        }

        public AccommodationStatistic Statistic
        {
            get => _statistic;
            set
            {
                if (value == _statistic) return;
                _statistic = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccommodationReservation> Reservations
        {
            get => _reservations;
            set
            {
                if (_reservations == value) return;
                _reservations = value;
                OnPropertyChanged();
            }
        } 

        public ObservableCollection<AccommodationRenovation> Renovations
        {
            get => _renovations;
            set
            {
                if (_renovations == value) return;
                _renovations = value;
                OnPropertyChanged();
            }
        }

        public AccommodationDetailsViewModel(User user, Accommodation accommodation)
        {
            _user = user;
            Accommodation = accommodation;

            _reservationService = Injector.GetService<AccommodationReservationService>();
            _statisticService = Injector.GetService<AccommodationStatisticService>();
            _ratingService = Injector.GetService<OwnerRatingService>();
            _renovationService = Injector.GetService<AccommodationRenovationService>();

            Reservations = new(_reservationService.GetAllFutureByAccommodationId(Accommodation.Id));
            Statistic = _statisticService.GetYearlyStatistic(accommodation, DateTime.Now.Year);
            Accommodation.Rating = _ratingService.CalculateRating(Accommodation);
            Renovations = new(_renovationService.GetAllFutureByAccommodationId(Accommodation.Id));
        }

    }
}
