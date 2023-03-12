using System.Windows;
using SIMSProject.View.OwnerViews;
using SIMSProject.Controller;
using SIMSProject.Model;

namespace SIMSProject.View
{
    /// <summary>
    /// Interaction logic for InitialWindow.xaml
    /// </summary>
    public partial class InitialWindow : Window
    {

        public Accommodation Accommodation { get; set; }
        public static AccommodationController AccommodationController { get; set; }
        public InitialWindow()
        {
            InitializeComponent();
        }

        private void Guest1_Click(object sender, RoutedEventArgs e)
        {
            Guest1.AccommodationSearchAndShowForm accommondationSearchAndShowForm = new();
            accommondationSearchAndShowForm.Show();
        }

        private void Guest2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Owner_Click(object? sender, RoutedEventArgs? e)
        {
            OwnerInitialWindow window = new();
            window.Show();
        }

        private void TourGuide_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
