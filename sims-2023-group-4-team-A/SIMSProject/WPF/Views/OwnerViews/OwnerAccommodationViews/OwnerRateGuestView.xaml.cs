using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerRateGuestView : Page
    {
        private User _user;
        private OwnerAllReservationsView _allReservationsView;
        private OwnerRateGuestViewModel _viewModel;

        public OwnerRateGuestView(User user, AccommodationReservation reservation, OwnerAllReservationsView allReservationsView)
        {
            InitializeComponent();
            _user = user;
            _allReservationsView = allReservationsView;
            _viewModel = new(user, reservation, this);
            DataContext = _viewModel;
        }

        internal void GoBackAndReload()
        {
            NavigationService?.GoBack();
            _allReservationsView.ReloadReservations();
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
