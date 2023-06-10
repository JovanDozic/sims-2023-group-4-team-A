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
        public Guest2 User = new();
        private readonly TourReservationsViewModel _tourReservationsViewModel;
        public TourReservations(Guest2 user)
        {
            InitializeComponent();
            User = user;
            _tourReservationsViewModel = new(user);
            this.DataContext = _tourReservationsViewModel;
        }

        private void RateGuide_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RateGuide(User, _tourReservationsViewModel.SelectedTourReservation, _tourReservationsViewModel.GetGuideId()));
            _tourReservationsViewModel.LoadReservationsByGuestId(User.Id);
            DgrReservations.SelectedItem = null;
            DgrReservations.Items.Refresh();
        }
        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShowKeyPoint(User, _tourReservationsViewModel.SelectedTourReservation));
        }
        
    }
}