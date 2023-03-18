using System.Windows;
using SIMSProject.View.OwnerViews;
using SIMSProject.View.GuideViews;
using SIMSProject.View.Guest2;
using SIMSProject.View.Guest1;
using SIMSProject.Model.UserModel;

namespace SIMSProject.View
{
    public partial class InitialWindow : Window
    {
        public Guest Guest { get; set; }

        public InitialWindow()
        {
            InitializeComponent();
        }

        private void Guest1_Click(object sender, RoutedEventArgs e)
        {
            AccommodationSearchAndShowForm accommondationSearchAndShowForm = new(Guest);
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
            TourCreation tourCreation = new TourCreation();
            tourCreation.Show();
        }
    }
}
