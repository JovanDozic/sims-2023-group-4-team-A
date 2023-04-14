using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class GuestRatingViewModel : ViewModelBase
    {
        private readonly User _user;
        private GuestRating _rating = new();
        private GuestRatingService _ratingService;

        public AccommodationReservation Reservation
        {
            get => _rating.Reservation;
            set
            {
                if (_rating.Reservation == value) return;
                _rating.Reservation = value;
                OnPropertyChanged();
            }
        }
        public int CleanlinessRating
        {
            get => _rating.CleanlinessRating;
            set
            {
                if (_rating.CleanlinessRating == value || value is < 1 or > 5) return;
                _rating.CleanlinessRating = value;
                OnPropertyChanged();
            }
        }
        public int ComplianceWithRules
        {
            get => _rating.ComplianceWithRules;
            set
            {
                if (_rating.ComplianceWithRules == value || value is < 1 or > 5) return;

                _rating.ComplianceWithRules = value;
                OnPropertyChanged();
            }
        }
        public int PaymentAndBilling
        {
            get => _rating.PaymentAndBilling;
            set
            {
                if (value == _rating.PaymentAndBilling || value is < 1 or > 5) return;
                _rating.PaymentAndBilling = value;
                OnPropertyChanged();
            }
        }
        public int CommunicationRating
        {
            get => _rating.CommunicationRating;
            set
            {
                if (value == _rating.CommunicationRating || value is < 1 or > 5) return;
                _rating.CommunicationRating = value;
                OnPropertyChanged();
            }
        }
        public int Recommendation
        {
            get => _rating.Recommendation;
            set
            {
                if (value == _rating.Recommendation || value is < 1 or > 5) return;
                _rating.Recommendation = value;
                OnPropertyChanged();
            }
        }
        public double Overall
        {
            get => _rating.Overall;
            set
            {
                if (value == _rating.Overall) return;
                _rating.Overall = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get => _rating.Comment;
            set
            {
                if (_rating.Comment == value) return;
                _rating.Comment = value;
                OnPropertyChanged();
            }
        }

        public GuestRatingViewModel(User user, AccommodationReservation reservation)
        {
            _user = user;
            _ratingService = Injector.GetService<GuestRatingService>();
            Reservation = reservation;
        }

        public void LeaveGuestRating()
        {
            Reservation.GuestRated = true;
            _ratingService.LeaveRating(_rating);
        }

    }
}
