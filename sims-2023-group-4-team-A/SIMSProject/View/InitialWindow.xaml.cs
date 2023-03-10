using System.Security.Cryptography.Xml;
using System.Windows;
using SIMSProject.View.OwnerViews;

namespace SIMSProject.View
{
    /// <summary>
    /// Interaction logic for InitialWindow.xaml
    /// </summary>
    public partial class InitialWindow : Window
    {
        public InitialWindow()
        {
            InitializeComponent();
        }

        private void Guest1_Click(object sender, RoutedEventArgs e)
        {

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
