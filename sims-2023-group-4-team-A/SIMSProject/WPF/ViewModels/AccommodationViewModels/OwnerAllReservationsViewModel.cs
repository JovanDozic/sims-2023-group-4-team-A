using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerForumViews;
using SIMSProject.WPF.Views.OwnerViews;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerAllReservationsViewModel : ViewModelBase
    {
        private User _user;
        private OwnerAllReservationsView _reservationsView;
        private AccommodationReservationService _reservationService;
        private OwnerRatingService _ownerRatingService;
        private GuestRatingService _guestRatingService;

        public Accommodation Accommodation { get; set; }
        public ObservableCollection<AccommodationReservation> Reservations { get; set; } = new();
        public AccommodationReservation? HoveredReservation { get; set; }



        public RelayCommand RateGuestCommand { get; set; }
        public RelayCommand ViewOwnerRatingCommand { get; set; }




        public OwnerAllReservationsViewModel(User user, Accommodation accommodation, OwnerAllReservationsView reservationsView)
        {
            _user = user;
            _reservationsView = reservationsView;
            Accommodation = accommodation;

            _reservationService = Injector.GetService<AccommodationReservationService>();
            _ownerRatingService = Injector.GetService<OwnerRatingService>();
            _guestRatingService = Injector.GetService<GuestRatingService>();

            RateGuestCommand = new RelayCommand(RateGuest, CanRateGuest);
            ViewOwnerRatingCommand = new RelayCommand(ViewOwnerRating);

            Reservations = new(_reservationService.GetAllByAccommodationId(Accommodation.Id).OrderByDescending(x => x.StartDate));
            MapRatings();
        }

        private bool CanViewOwnerRating()
        {
            return true;
        }
        private void ViewOwnerRating()
        {
            if (HoveredReservation is null) return;
            OwnerOwnerRatingView ownerRatingView = new(_user, HoveredReservation);
            OwnerWindow ownerWindow = Window.GetWindow(_reservationsView) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(ownerRatingView);
        }

        private bool CanRateGuest()
        {
            return true;
        }
        public void RateGuest()
        {
            if (HoveredReservation is null) return;
            OwnerRateGuestView rateGuestView = new(_user, HoveredReservation);
            OwnerWindow ownerWindow = Window.GetWindow(_reservationsView) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(rateGuestView);
        }

        public void MapRatings()
        {
            foreach(var reservation in Reservations)
            {
                if (reservation.OwnerRated) reservation.OwnerRating = _ownerRatingService.GetByReservationId(reservation.Id).Overall;
                if (reservation.GuestRated) reservation.GuestRating = _guestRatingService.GetByReservationId(reservation.Id).Overall;
            }
        }

    }
}
