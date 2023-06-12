using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    internal class OwnerAllOwnerRatingsViewModel : ViewModelBase
    {
        private User _user;
        private OwnerAllOwnerRatingsView _allView;
        private OwnerRatingService _ratingService;

        public Accommodation Accommodation { get; set; }
        public ObservableCollection<OwnerRating> Ratings { get; set; } = new();
        public OwnerRating Rating { get; set; } = new();

        public OwnerAllOwnerRatingsViewModel(User user, Accommodation accommodation, OwnerAllOwnerRatingsView allView)
        {
            _user = user;
            Accommodation = accommodation;
            _allView = allView;

            _ratingService = Injector.GetService<OwnerRatingService>();

            LoadRatings();
        }

        internal void LoadRatings()
        {
            Ratings = new(_ratingService.GetAllByAccommodationId(Accommodation.Id)
                                             .OrderByDescending(x => x.Reservation.EndDate)
                                             .OrderBy(x => x.Reservation.OwnerRated));
        }
    }
}
