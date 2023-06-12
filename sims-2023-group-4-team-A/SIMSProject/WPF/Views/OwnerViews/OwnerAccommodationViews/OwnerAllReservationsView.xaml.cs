using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAllReservationsView : Page, INavigationService
    {
        private User _user;
        private OwnerAllReservationsViewModel _viewModel;

        public OwnerAllReservationsView(User user, Accommodation accommodation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, accommodation, this);
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

        private void BtnViewOwnerRating_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnReservationInFuture_Click(object sender, RoutedEventArgs e)
        {
            Point mousePosition = Mouse.GetPosition(PopupReservationInFuture);
            PopupReservationInFuture.HorizontalOffset = mousePosition.X - 20;
            PopupReservationInFuture.VerticalOffset = mousePosition.Y - 10;
            PopupReservationInFuture.IsOpen = true;
        }

        private void LstReservationsItem_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem ?? throw new System.Exception("List item not found.");
            AccommodationReservation? reservation = listViewItem.DataContext as AccommodationReservation;
            _viewModel.HoveredReservation = reservation;
        }

        private void BtnOwnerRatingNotAvailable_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Popup like one above
        }

        internal void ReloadReservations()
        {
            _viewModel.LoadReservations();
            LstReservations.Items.Refresh();
        }
    }
}
