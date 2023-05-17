using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerAllReservationsView : Page, INavigationService
    {
        private User _user;
        private AccommodationReservationViewModel _viewModel;

        public OwnerAllReservationsView(User user, Accommodation accommodation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, accommodation);
            DataContext = _viewModel;

            _viewModel.LoadReservationsByAccommodation();
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
            MessageBox.Show("Videces posle al evo ovo:\n" + _viewModel.SelectedReservation.ToString());
        }

        private void BtnReservationInFuture_Click(object sender, RoutedEventArgs e)
        {
            Point mousePosition = Mouse.GetPosition(PopupReservationInFuture);
            PopupReservationInFuture.HorizontalOffset = mousePosition.X - 20;
            PopupReservationInFuture.VerticalOffset = mousePosition.Y - 10;
            PopupReservationInFuture.IsOpen = true;
        }
    }
}
