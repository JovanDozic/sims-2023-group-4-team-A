using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAccommodationDetails : Page, INavigationService
    {
        private User _user;
        private readonly AccommodationDetailsViewModel _viewModel;

        public OwnerAccommodationDetails(User user, Accommodation accommodation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, accommodation);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        public void GoBack()
        {
            NavigationService?.GoBack();
        }

        private void BtnReservationHelp_Click(object sender, RoutedEventArgs e)
        {
            PopupReservationHelp.IsOpen = true;
        }

        private void BtnRenovationHelp_Click(object sender, RoutedEventArgs e)
        {
            PopupRenovationHelp.IsOpen = true;
        }

        private void BtnAllStats_Click(object sender, RoutedEventArgs e)
        {
            OwnerYearlyStatisticsView yearlyStatisticsView = new(_viewModel.Accommodation);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(yearlyStatisticsView);
        }

        private void BtnAllReservations_Click(object sender, RoutedEventArgs e)
        {
            OwnerAllReservationsView allReservationView = new(_user, _viewModel.Accommodation);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(allReservationView);
        }

        private void BtnAllRenovations_Click(object sender, RoutedEventArgs e)
        {
            OwnerAllRenovationsView allRenovationsView = new(_user, _viewModel.Accommodation, this);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(allRenovationsView);
        }

        private void BtnScheduleRenovation_Click(object sender, RoutedEventArgs e)
        {
            OwnerScheduleRenovationView scheduleRenovationView = new(_user, _viewModel.Accommodation, this);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(scheduleRenovationView);
        }

        internal void ReloadRenovations()
        {
            _viewModel.ReloadRenovations();
        }

        private void BtnReschedulingRequests_Click(object sender, RoutedEventArgs e)
        {
            OwnerAllReschedulingRequestsView requestsView = new(_user, _viewModel.Accommodation);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(_user);
            ownerWindow?.SwitchToPage(requestsView);
        }
    }
}
