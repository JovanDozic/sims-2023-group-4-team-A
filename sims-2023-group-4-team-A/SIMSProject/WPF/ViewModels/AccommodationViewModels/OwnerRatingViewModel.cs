using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class OwnerRatingViewModel : ViewModelBase
    {
        private readonly User _user;
        private OwnerRating _rating = new();
        private OwnerRatingService _ratingService;
        private AccommodationReservationService _reservationService;
        private Accommodation _accommodation = new();
        private string _selectedImageFile = string.Empty;
        private string _ownerNameTB = string.Empty;

        public ObservableCollection<AccommodationReservation> Reservations { get; set; } = new();
        public object ReservationsCombo { get; private set; } = new();

        public string OwnerNameTextBlock
        {
            get => _ownerNameTB;
            set
            {
                if (value == _ownerNameTB) return;
                _ownerNameTB = value;
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
        public string ImageURLsCSV
        {
            get => _rating.ImageURLsCSV;
            set
            {
                if (_rating.ImageURLsCSV == value) return;
                _rating.ImageURLsCSV = value;
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
        public int OwnerCorrectness
        {
            get => _rating.OwnerCorrectness;
            set
            {
                if (_rating.OwnerCorrectness == value || value is < 1 or > 5) return;
                _rating.OwnerCorrectness = value;
                OnPropertyChanged();
            }
        }
        public int Kindness
        {
            get => _rating.Kindness;
            set
            {
                if (_rating.Kindness == value || value is < 1 or > 5) return;
                _rating.Kindness = value;
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
        public List<string> ImageURLs
        {
            get => _rating.ImageURLs;
            set
            {
                if (_rating.ImageURLs == value) return;
                _rating.ImageURLs = value;
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
            _reservationService = Injector.GetService<AccommodationReservationService>();
            Reservation = reservation;
            LoadRating();
        }

        public void AddReservationsToCombo()
        {
            foreach (var reservation in _reservationService.GetAll())
            {
                if (reservation.EndDate < DateTime.Today &&
                    reservation.EndDate.AddDays(5) >= DateTime.Today &&
                    !reservation.Canceled &&
                    reservation.Guest.Id == _user.Id &&
                    !reservation.OwnerRated)     
                {
                    reservation.ReservationDetails = string.Format("{0} ({1} - {2})", reservation.Accommodation.Name, reservation.StartDate.ToShortDateString(), reservation.EndDate.ToShortDateString());
                    Reservations.Add(reservation);
                }
            }
            ReservationsCombo = Reservations;
        }

        public string GetOwnerUsername()
        {
            return SelectedReservation.Accommodation.Owner.Username;
        }

        public bool IsSelected()
        {
            return SelectedReservation != null;
        }

        public void UploadImage(string imageUrl)
        {
            ImageURLs.Add(imageUrl);
        }

        public void RateOwnerAndAccommodation()
        {
            SelectedReservation.OwnerRated = true;
            _ratingService.LeaveRating(_rating);
        }

        public void LoadRating()
        {
            if (Reservation == null) return;
            Rating = _ratingService.GetByReservationId(Reservation.Id);
        }
    }
}
