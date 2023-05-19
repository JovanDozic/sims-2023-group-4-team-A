using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.TeamFoundation.Build.WebApi;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.OwnerViews;
using SIMSProject.WPF.ViewModels.OwnerViewModels;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerHomeViewOld : Window
    {
        private User _user { get; set; }
        private OwnerHomeViewModel _viewModel { get; set; }

        public OwnerHomeViewOld(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _user = new Owner(0, "<null>", "<null>", DateTime.Now);
            SignInView window = new();
            window.Show();
            Close();
        }

        private void BtnReschedulingRequests_Click(object sender, RoutedEventArgs e)
        {
            ReschedulingRequestsView window = new(_user);
            window.ShowDialog();
            _viewModel.LoadReservations();
        }

        private void DgrAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.LoadReservations();
            _viewModel.LoadRenovations();
            DgrReservations.Items.Refresh();
            DgrRenovations.Items.Refresh();
        }

        private void DgrReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnRateGuest.IsEnabled = _viewModel.IsGuestRatingEnabled();
            BtnViewOwnerRating.IsEnabled = _viewModel.IsOwnerRatingEnabled();
        }

        private void DgrRenovations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnCancelRenovation.IsEnabled = _viewModel.IsRenovationCancelationEnabled(DgrRenovations.SelectedItem as AccommodationRenovation);
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
            DgrReservations.SelectedItem = null;
        }

        private void BtnViewOwnerRating_Click(object sender, RoutedEventArgs e)
        {
            OwnerRatingView window = new(_user, _viewModel.SelectedReservation);
            window.ShowDialog();
        }

        private void OwnerInfo_Click(object sender, RoutedEventArgs e)
        {
            var popupWindow = new Window
            {
                Title = "Status vlasnika",
                Top = 375,
                Left = 275,
                Height = 200,
                Width = 350,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.SingleBorderWindow,
                Content = new OwnerAccountPopupView(_user)
            };
            popupWindow.ShowDialog();
        }

        private void BtnNotifications_Click(object sender, RoutedEventArgs e)
        {
            var popupWindow = new Window
            {
                Title = "Obaveštenja",
                Height = 450,
                Width = 600,
                Top = (SystemParameters.FullPrimaryScreenHeight - 450) / 2,
                Left = (SystemParameters.FullPrimaryScreenWidth - 600) / 2,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.SingleBorderWindow,
                Content = new OwnerNotificationView(_user)
            };
            popupWindow.ShowDialog();
        }

        private void BtnCancelRenovation_Click(object sender, RoutedEventArgs e)
        {
            if (DgrRenovations.SelectedItem is null) return;

            if (MessageBox.Show("Da li ste sigurni da želite da otkažete renoviranje?", 
                                "Otkazivanje renoviranja", 
                                MessageBoxButton.YesNo, 
                                MessageBoxImage.Question) == MessageBoxResult.No) 
                return;

            _viewModel.CancelRenovation();
            _viewModel.LoadRenovations();
            DgrRenovations.Items.Refresh();
            DgrRenovations.SelectedItem = null;
        }

        

        private void BtnScheduleRenovation_Click(object sender, RoutedEventArgs e)
        {
            if (DgrAccommodations.SelectedItem is null)
            {
                MessageBox.Show("Izaberite smeštaj za koji želite da yakažete renoviranje", "Izaberite smeštaj!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } 
            OwnerScheduleRenovationOld window = new(_user, _viewModel.SelectedAccommodation);
            window.ShowDialog();
            _viewModel.LoadRenovations();
            DgrRenovations.Items.Refresh();
            DgrRenovations.SelectedItem = null;
        }
    }
}