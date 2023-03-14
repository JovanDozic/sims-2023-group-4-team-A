using System.Windows;
using SIMSProject.View.OwnerViews;
using SIMSProject.Controller;
using SIMSProject.Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMSProject.View.Guest2;
using SIMSProject.View.Guest1;
using SIMSProject.View.GuideViews;


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
            OwnerInitialWindow window = new();
            window.Show();
        }

        private void TourGuide_Click(object sender, RoutedEventArgs e)
        {
            TourCreation tourCreation = new TourCreation();
            tourCreation.Show();
            
            
        }
    }
}
