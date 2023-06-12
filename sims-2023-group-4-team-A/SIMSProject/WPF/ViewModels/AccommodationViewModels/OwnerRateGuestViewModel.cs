using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerRateGuestViewModel
    {
        private User _user;
        private OwnerRateGuestView _view;

        public AccommodationReservation Reservation { get; set; }
        public GuestRating Rating { get; set; } = new();

        public OwnerRateGuestViewModel(User user, AccommodationReservation reservation, OwnerRateGuestView ownerRateGuestView)
        {
            _user = user;
            Reservation = reservation;
            _view = ownerRateGuestView;
        }
    }
}
