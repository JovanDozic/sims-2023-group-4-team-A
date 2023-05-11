using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerMonthlyStatisticsView : Page, INavigationService
    {
        private AccommodationStatisticsViewModel _viewModel;

        public OwnerMonthlyStatisticsView(Accommodation accommodation, AccommodationStatistic statistic)
        {
            InitializeComponent();
            _viewModel = new(accommodation, statistic);
            DataContext = _viewModel;

            _viewModel.LoadMonthlyStatistics();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        public void GoBack()
        {
            NavigationService?.GoBack();
        }
    }
}
