using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourReservations.xaml
    /// </summary>
    public partial class TourReservations : Page
    {
        public Guest User = new();
        private readonly TourReservationsViewModel _tourReservationsViewModel;
        public TourReservations(Guest user)
        {
            InitializeComponent();
            User = user;
            _tourReservationsViewModel = new(User);
            _tourReservationsViewModel.LoadReservationsByGuestId(User.Id);
            this.DataContext = _tourReservationsViewModel;
        }

        private void RateGuide_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RateGuide(User, _tourReservationsViewModel.SelectedTourReservation, _tourReservationsViewModel.GetGuideId()));
            _tourReservationsViewModel.LoadReservationsByGuestId(User.Id);
            DgrReservations.SelectedItem = null;
            DgrReservations.Items.Refresh();
        }
        private void ShowKeyPoint_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShowKeyPoint(User, _tourReservationsViewModel.SelectedTourReservation));
        }
        private void SetButtonState(object sender, bool state)
        {
            if (sender is not Button button) return;
            button.IsEnabled = state;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetButtonState(BTNRateGuide, _tourReservationsViewModel.IsRatingEnabled());
            SetButtonState(BTNShowKeyPoint, _tourReservationsViewModel.IsTourActive());
        }

        
    }
}