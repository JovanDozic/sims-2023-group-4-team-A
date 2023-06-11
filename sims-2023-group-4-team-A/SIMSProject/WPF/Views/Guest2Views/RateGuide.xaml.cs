using System.Windows.Controls;
using System.Windows.Navigation;
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
        public RateGuide(User user, TourReservation tourReservation, int guideId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new GuideRatingViewModel(user, tourReservation, guideId, navigationService);
        }

    }
}
