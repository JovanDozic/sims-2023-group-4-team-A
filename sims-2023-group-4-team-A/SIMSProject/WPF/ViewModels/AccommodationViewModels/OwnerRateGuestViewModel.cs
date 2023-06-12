using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerRateGuestViewModel : ViewModelBase
    {
        private User _user;
        private App _app = (App)System.Windows.Application.Current;
        private OwnerRateGuestView _view;
        private GuestRatingService _guestRatingService;
        private GuestRating _rating = new();

        public AccommodationReservation Reservation { get; set; }
        public GuestRating Rating
        {
            get => _rating;
            set
            {
                if (value == _rating) return;
                _rating = value;
                OnPropertyChanged(nameof(Rating));
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
                OnPropertyChanged(nameof(Overall));
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
                OnPropertyChanged(nameof(Overall));
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
                OnPropertyChanged(nameof(Overall));
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
                OnPropertyChanged(nameof(Overall));
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
                OnPropertyChanged(nameof(Overall));
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
                OnPropertyChanged(nameof(Overall));
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







        public RelayCommand RateGuestCommand { get; set; }

        public OwnerRateGuestViewModel(User user, AccommodationReservation reservation, OwnerRateGuestView ownerRateGuestView)
        {
            _user = user;
            Reservation = reservation;
            _view = ownerRateGuestView;

            _guestRatingService = Injector.GetService<GuestRatingService>();

            RateGuestCommand = new RelayCommand(RateGuest, CanRateGuest);
        }

        private void RateGuest()
        {

            

            if (_app.CurrentLanguage == "en-US")
            {
                if (MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }
            else
            {
                if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
            }

            Reservation.GuestRated = true;
            Rating.Reservation = Reservation;

            Rating = _guestRatingService.LeaveRating(Rating);

            if (_app.CurrentLanguage == "en-US")
            {
                MessageBox.Show("Guest rated successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Gost uspesno ocenjen!", "Uspeh!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            _view.GoBackAndReload();
        }

        private bool CanRateGuest()
        {
            return true;
        }
    }
}
