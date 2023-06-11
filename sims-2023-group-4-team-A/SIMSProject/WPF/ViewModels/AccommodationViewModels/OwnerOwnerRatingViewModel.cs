using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using Xceed.Wpf.Toolkit;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerOwnerRatingViewModel : ViewModelBase
    {
        private User _user;
        private OwnerRatingService _ownerRatingService;

        public AccommodationReservation Reservation { get; set; }
        public OwnerRating Rating { get; set; }

        public OwnerOwnerRatingViewModel(User user, AccommodationReservation reservation)
        {
            _user = user;
            Reservation = reservation;

            _ownerRatingService = Injector.GetService<OwnerRatingService>();

            Rating = _ownerRatingService.GetByReservationId(Reservation.Id);
            OnPropertyChanged(nameof(Rating));
        }
    }
}
