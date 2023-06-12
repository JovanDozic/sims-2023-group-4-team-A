using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourRequestStatistics.xaml
    /// </summary>
    public partial class TourRequestStatistics : Page
    {
        public TourRequestStatistics(Guest2 user, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new TourRequestStatisticsViewModel(user, navigationService);
        }

    }
}
