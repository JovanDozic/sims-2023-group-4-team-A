using SIMSProject.WPF.ViewModels.TourViewModels;
using SIMSProject.WPF.ViewModels.TourViewModels.StatisticsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for TourStatisticsPage.xaml
    /// </summary>
    public partial class TourStatisticsPage : Page
    {
        private TourStatisticsViewModel ViewModel { get; set; }
        public TourStatisticsPage()
        {
            InitializeComponent();
            ViewModel = new TourStatisticsViewModel();
            this.DataContext = ViewModel;
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new IndividualStatisticsPage());
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
