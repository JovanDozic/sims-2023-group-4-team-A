using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAllOwnerRatingsView : Page
    {
        private User _user;
        private OwnerAllOwnerRatingsViewModel _viewModel;

        public OwnerAllOwnerRatingsView(User user, Accommodation accommodation)
        {
            InitializeComponent();

            _user = user;
            _viewModel = new(_user, accommodation, this);
            DataContext = _viewModel;
        }

        private void LstRatings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstRatings.SelectedItem is null) return;
            if (_viewModel.Rating.Reservation.DisplayOwnerRatingNotAvailable)
            {
                OwnerRateGuestView rateGuestView = new(_user, _viewModel.Rating.Reservation, null, this);
                OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
                ownerWindow.MainFrame.Navigate(rateGuestView);
                return;
            }
            else
            {
                OwnerOwnerRatingView ownerRatingView = new(_user, _viewModel.Rating.Reservation);
                OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
                ownerWindow.MainFrame.Navigate(ownerRatingView);
            }
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        internal void ReloadRatings()
        {
            _viewModel.LoadRatings();
            LstRatings.Items.Refresh();
            LstRatings.SelectedIndex = -1;
        }

    }
}
