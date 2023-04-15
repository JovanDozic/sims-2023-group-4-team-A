using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerRatingViewModel : ViewModelBase
    {
        private readonly User _user;
        private OwnerRating _rating = new();
        private OwnerRatingService _ratingService;
        private string _selectedImageFile = string.Empty;

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

        public string SelectedImageFile
        {
            get => _selectedImageFile;
            set
            {
                if (_selectedImageFile == value) return;
                _selectedImageFile = value;
                OnPropertyChanged();
            }
        }

        public OwnerRatingViewModel(User user, AccommodationReservation reservation)
        {
            _user = user;
            _ratingService = Injector.GetService<OwnerRatingService>();
            Reservation = reservation;

            LoadRating();
        }

        public void LoadRating()
        {
            Rating = _ratingService.GetByReservationId(Reservation.Id);
        }
    }
}
