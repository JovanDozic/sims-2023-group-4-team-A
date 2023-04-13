using System.Windows;
using System.Windows.Controls;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.OwnerViews;
using SIMSProject.WPF.ViewModels.OwnerViewModels;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerHomeView : Window
    {
        private User _user { get; set; }
        private OwnerHomeViewModel _viewModel { get; set; }

        public OwnerHomeView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;

            // TODO: call unrated guest check
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _user = new Owner(0, "<null>", "<null>");
            SignInView window = new();
            window.Show();
            Close();
        }

        private void BtnReschedulingRequests_Click(object sender, RoutedEventArgs e)
        {
            ReschedulingRequestsView window = new(_user);
            window.ShowDialog();
        }

        private void DgrAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.LoadReservations();
            DgrReservations.Items.Refresh();
        }

        private void DgrReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnRateGuest.IsEnabled = _viewModel.IsGuestRatingEnabled();
        }

        private void BtnRegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationView window = new(_user);
            window.ShowDialog();
            _viewModel.LoadAccommodations();
            DgrAccommodations.Items.Refresh();
        }

        private void BtnRateGuest_Click(object sender, RoutedEventArgs e)
        {
            RateGuestView window = new(_user, _viewModel.SelectedReservation);
            window.ShowDialog();
            BtnRateGuest.IsEnabled = false;
            _viewModel.LoadReservations();
            DgrReservations.Items.Refresh();
        }
    }
}