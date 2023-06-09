using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for ShowKeyPoint.xaml
    /// </summary>
    public partial class ShowKeyPoint : Page
    {
        public ShowKeyPoint(User user, TourReservation tourReservation)
        {
            InitializeComponent();
            DataContext = new TourReservationDetailsViewModel(user, tourReservation);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
