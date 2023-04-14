using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.OwnerViewModels
{
    public class OwnerHomeViewModel : ViewModelBase
    {
        private readonly User _user;
        public readonly AccommodationViewModel _accommodationViewModel;
        private readonly AccommodationReservationViewModel _reservationViewModel;
        private ObservableCollection<Accommodation> _accommodations = new();
        private ObservableCollection<AccommodationReservation> _selectedAccommodationReservations = new();

        public Accommodation SelectedAccommodation { get; set; } = new();
        public AccommodationReservation SelectedReservation { get; set; } = new();
        public ObservableCollection<Accommodation> Accommodations
        {
            get { return _accommodations; }
            set
            {
                if (_accommodations == value) return;
                _accommodations = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AccommodationReservation> SelectedAccommodationReservations
        {
            get { return _selectedAccommodationReservations; }
            set
            {
                if (_selectedAccommodationReservations == value) return;
                _selectedAccommodationReservations = value;
                OnPropertyChanged();
            }
        }

        public OwnerHomeViewModel(User user)
        {
            _user = user;
            _accommodationViewModel = new(_user);
            _reservationViewModel = new(_user);

            LoadAccommodations();
        }

        public void LoadReservations()
        {
            if (SelectedAccommodation == null) return;
            SelectedAccommodationReservations = _reservationViewModel.LoadReservationsByAccommodation(SelectedAccommodation);
        }

        public void LoadAccommodations()
        {
            if (Accommodations == null) return;
            Accommodations = _accommodationViewModel.LoadAccommodationsByOwner();
        }

        public bool IsGuestRatingEnabled()
        {
            if (SelectedReservation == null) return false;
            if (SelectedReservation.GuestRated || 
                DateTime.Now < SelectedReservation.EndDate || 
                DateTime.Now > SelectedReservation.EndDate.AddDays(5))
            {
                return false;
            }
            return true;
        }

        public bool IsOwnerRatingEnabled()
        {
            if (SelectedReservation == null) return false;
            else if (!SelectedReservation.GuestRated && IsGuestRatingEnabled()) return false;
            else if (!SelectedReservation.OwnerRated) return false;
            return true;
        }

    }
}
