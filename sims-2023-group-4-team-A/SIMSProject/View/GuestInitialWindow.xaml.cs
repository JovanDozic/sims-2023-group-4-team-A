using System.Windows;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.Guest1;
using SIMSProject.View.Guest2;
using SIMSProject.WPF.Views.Guest2Views;

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
            //ShowAndSearchTours window = new(User);
            Guest2HomeView window = new(User);
            window.Show();
            Close();
        }
    }
}