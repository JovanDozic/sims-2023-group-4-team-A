using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;

namespace SIMSProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for RateGuide.xaml
    /// </summary>
    public partial class RateGuide : Page
    {
        public RateGuide(User user, TourReservation tourReservation, int guideId)
        {
            InitializeComponent();
            DataContext = new GuideRatingViewModel(user, tourReservation, guideId); ;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
