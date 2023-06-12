using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerGuestRatingViewModel : ViewModelBase
    {
        private User _user;
        private App _app = (App)System.Windows.Application.Current;
        private OwnerGuestRatingView _view;
        private GuestRatingService _guestRatingService;

        public GuestRating Rating { get; set; }

        public OwnerGuestRatingViewModel(User user, AccommodationReservation reservation, OwnerGuestRatingView ownerGuestRatingView)
        {
            _user = user;
            _view = ownerGuestRatingView;

            _guestRatingService = Injector.GetService<GuestRatingService>();

            Rating = _guestRatingService.GetByReservationId(reservation.Id);
            OnPropertyChanged(nameof(Rating));
        }
    }
}
