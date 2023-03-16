using System.Windows;
using SIMSProject.View.OwnerViews;
using SIMSProject.View.GuideViews;
using SIMSProject.View.Guest2;
using SIMSProject.View.Guest1;

namespace SIMSProject.View
{
    public partial class InitialWindow : Window
    {
        public InitialWindow()
        {
            InitializeComponent();
        }

        private void Guest1_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearchAndShowForm accommondationSearchAndShowForm = new();
            accommondationSearchAndShowForm.Show();
        }

        private void Guest2_Click(object sender, RoutedEventArgs e)
        {
            ShowAndSearchTours showAndSearchTours = new ShowAndSearchTours();
            showAndSearchTours.Show();
        }

        private void Owner_Click(object? sender, RoutedEventArgs? e)
        {
            OwnerInitialWindow ownerInitialWindow = new();
            ownerInitialWindow.Show();
        }

        private void TourGuide_Click(object sender, RoutedEventArgs e)
        {
            GuideInitialWindow window = new();
            window.Show();
            
            
        }
    }
}
