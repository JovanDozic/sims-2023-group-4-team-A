using SIMSProject.WPF.ViewModels.TourViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for TourStatisticsPage.xaml
    /// </summary>
    public partial class TourStatisticsPage : Page
    {
        private ToursViewModel ViewModel { get; set; }
        public TourStatisticsPage()
        {
            InitializeComponent();
            ViewModel = new ToursViewModel();
            this.DataContext = ViewModel;

        }

        //private void rdbYear_Checked(object sender, RoutedEventArgs e)
        //{
        //    ViewModel.GetStatistics(int.Parse(tbYear.Text));
        //}
    }
}
