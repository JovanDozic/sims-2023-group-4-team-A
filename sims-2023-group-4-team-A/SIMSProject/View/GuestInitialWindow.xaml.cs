using System.Windows;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.Guest1;
using SIMSProject.View.Guest2;

namespace SIMSProject.View
{
    public partial class GuestInitialWindow : Window
    {
        public Guest User = new();

        public GuestInitialWindow(Guest user)
        {
            InitializeComponent();
            User = user;
        }

        private void OpenAccommodations_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearchAndShowForm window = new(User);
            window.Show();
        }

        private void OpenTours_Click(object sender, RoutedEventArgs e)
        {
            ShowAndSearchTours window = new(User);
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new AccommodationAndOwnerRating(User);
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new ReservationReqeusts(User);
            window.Show();
        }
    }
}