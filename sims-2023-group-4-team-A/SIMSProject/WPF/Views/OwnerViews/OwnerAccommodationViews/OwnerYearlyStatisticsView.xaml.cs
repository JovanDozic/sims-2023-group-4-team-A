using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerYearlyStatisticsView : Page, INavigationService
    {
        private AccommodationStatisticsViewModel _viewModel;

        public OwnerYearlyStatisticsView(Accommodation accommodation)
        {
            InitializeComponent();
            _viewModel = new(accommodation);
            DataContext = _viewModel;

            _viewModel.LoadYearlyStatistics();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        public void GoBack()
        {
            NavigationService?.GoBack();
        }

        private void LstStats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OwnerMonthlyStatisticsView monthlyStatisticsView = new(_viewModel.Accommodation, _viewModel.Statistic);
            OwnerWindow ownerWindow = Window.GetWindow(this) as OwnerWindow ?? new(new User());
            ownerWindow?.SwitchToPage(monthlyStatisticsView);
        }
    }
}
